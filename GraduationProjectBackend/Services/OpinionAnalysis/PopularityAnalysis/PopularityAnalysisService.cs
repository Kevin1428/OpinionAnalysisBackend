﻿using GraduationProjectBackend.ConfigModel;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Enums;
using GraduationProjectBackend.Helper.Redis;
using GraduationProjectBackend.Services.OpinionAnalysis.WordCloud;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace GraduationProjectBackend.Services.OpinionAnalysis.PopularityAnalysis
{
    public class PopularityAnalysisService : ServiceBase, IPopularityAnalysisService
    {

        private readonly ArticleHelper _articleHelper;
        private readonly OpinionAnalysisConfig _opinionAnalysisConfig;

        public PopularityAnalysisService(ArticleHelper articleHelper, IOptions<OpinionAnalysisConfig> opinionAnalysisConfig, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _articleHelper = articleHelper;
            _opinionAnalysisConfig = opinionAnalysisConfig.Value;
        }

        public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode,
            IEnumerable<AddressType>? addressTypes)
        {

            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch, addressTypes);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                count = g.Sum(A => A.MessageCount!.All + 1)
            }).ToList();

            var allAddress = Enum.GetValues(typeof(AddressType));
            var addressDiscussNumber = new Dictionary<AddressType, int>();
            var redisAddress = new List<AddressArticelRedis>();

            foreach (AddressType value in allAddress)
            {
                var articleNumber = article.Count(o => o.AddressType == value);
                var messageNumber = article.SelectMany(o => o.Messages).Count(o => o.AddressType == value);
                addressDiscussNumber.Add(value, articleNumber + messageNumber);
            }

            var top3AddressTypes = addressDiscussNumber.OrderByDescending(o => o.Value).Take(3).Select(o => o.Key);
            foreach (var value in top3AddressTypes)
            {
                var addressTopArticle = article.MaxBy(o =>
                    (o.AddressType == value) ? 1 : 0 + (o.Messages?.Count(m => m.AddressType == value)));
                if (addressTopArticle != null)
                    redisAddress.Add(new AddressArticelRedis(value.ToString(), addressTopArticle.ArticleTitle, addressTopArticle.Content, addressTopArticle.ContentSentiment)!);
            }

            var dayRange = dateRange;
            var disCount = 0;
            var leftDate = startDate.AddDays(dayRange);
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var discussNumber = new List<int>();
            var hotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();
            var redisHotArticleContents = new Dictionary<DateOnly, ICollection<string>>();
            var redisHotArticleNewsContents = new Dictionary<DateOnly, ICollection<string>>();

            var wordAnalysisResults = new List<WordCloudAnalysisResult>();
            var wordCloudService = ServiceProvider.GetRequiredService<IWordCloudService>();

            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                var currentDateHotArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) >= leftDate && DateOnly.Parse(g.SearchDate) < rightDate)
                    .OrderByDescending(o => o.MessageCount!.All).Take(1).ToList();

                var currentDateHotNewsArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) >= leftDate && DateOnly.Parse(g.SearchDate) < rightDate && g.ArticleTitle.Contains("[新聞"))
                    .OrderByDescending(o => o.MessageCount!.All).Take(1).ToList();

                hotArticles.TryAdd(leftDate, currentDateHotArticles.Select(o => o.ToAtricleUserView()).ToList());
                redisHotArticleContents.TryAdd(leftDate, currentDateHotArticles.Select(o => o.Content).ToList()!);
                redisHotArticleNewsContents.TryAdd(leftDate, currentDateHotNewsArticles.Select(o => o.Content).ToList()!);

                disCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date < rightDate).Sum(g => g.count);

                discussNumber.Add(disCount);
                #region 斷詞統計
                wordAnalysisResults.Add(await wordCloudService.GetWordCloudResponse(
                    article.Where(g => DateOnly.Parse(g.SearchDate) >= leftDate && DateOnly.Parse(g.SearchDate) < rightDate).ToList(),
                    (a) => true,
                    (a) => true,
                    (a) => true));
                #endregion
                leftDate = rightDate;
                rightDate = rightDate.AddDays(dayRange);

            }

            var redisDatabase = RedisHelper.GetRedisDatabase();
            var redisKey = RedisHelper.GetRedisKey(topic,
                                                   startDate,
                                                   endDate,
                                                   dateRange,
                                                   isExactMatch,
                                                   searchMode,
                                                   addressTypes);

            await redisDatabase.HashSetAsync(redisKey, "PopularityNumber", JsonSerializer.Serialize(discussNumber.Sum()), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "Popularity", JsonSerializer.Serialize(redisHotArticleContents), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "PopularityNews", JsonSerializer.Serialize(redisHotArticleNewsContents), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "AddressArticle",
                JsonSerializer.Serialize(redisAddress), When.NotExists);

            redisDatabase.KeyExpire(redisKey, TimeSpan.FromSeconds(_opinionAnalysisConfig.RedisExpireSecond));

            return new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                  Dates: dateOfAnalysis,
                                                  HotArticles: hotArticles,
                                                  AddressDiscussNumber: addressDiscussNumber);
        }
        public new record AddressArticelRedis(string AddressName, string Title, string Content, string Sentiment);

    }
}

