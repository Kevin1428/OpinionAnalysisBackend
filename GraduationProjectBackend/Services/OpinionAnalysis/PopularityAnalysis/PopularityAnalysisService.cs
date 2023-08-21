using GraduationProjectBackend.ConfigModel;
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
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode)
        {

            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                count = g.Sum(A => A.MessageCount!.All)
            }).ToList();


            var dayRange = dateRange;
            var disCount = 0;
            var leftDate = startDate.AddDays(dayRange);
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var discussNumber = new List<int>();
            var hotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();
            var redisHotArticleContents = new Dictionary<DateOnly, ICollection<string>>();

            var wordAnalysisResults = new List<WordCloudAnalysisResult>();
            var wordCloudService = ServiceProvider.GetRequiredService<IWordCloudService>();

            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                var currentDateHotArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate)
                    .OrderByDescending(o => o.MessageCount!.All).Take(1).ToList();

                hotArticles.TryAdd(leftDate, currentDateHotArticles.Select(o => o.ToAtricleUserView()).ToList());
                redisHotArticleContents.TryAdd(leftDate, currentDateHotArticles.Select(o => o.Content).ToList()!);

                disCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.count);

                discussNumber.Add(disCount);
                #region 斷詞統計
                wordAnalysisResults.Add(await wordCloudService.GetWordCloudResponse(
                    article.Where(g => DateOnly.Parse(g.SearchDate) >= leftDate && DateOnly.Parse(g.SearchDate) <= rightDate).ToList(),
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
                                                   searchMode);
            await redisDatabase.HashSetAsync(redisKey, "Popularity", JsonSerializer.Serialize(redisHotArticleContents), When.NotExists);

            var b = redisDatabase.HashGet(redisKey, "Popularity");
            redisDatabase.KeyExpire(redisKey, TimeSpan.FromSeconds(_opinionAnalysisConfig.RedisExpireSecond));

            return new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                  Dates: dateOfAnalysis,
                                                  HotArticles: hotArticles,
                                                  WordCloudAnalysisResults: wordAnalysisResults);
        }

    }
}

