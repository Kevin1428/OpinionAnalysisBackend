using GraduationProjectBackend.ConfigModel;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Enums;
using GraduationProjectBackend.Helper.Redis;
using GraduationProjectBackend.Services.OpinionAnalysis.WordCloud;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace GraduationProjectBackend.Services.OpinionAnalysis.SentimentAnalysis
{
    public class SentimentAnalysisService : ServiceBase, ISentimentAnalysisService
    {
        private readonly ArticleHelper _articleHelper;
        private readonly OpinionAnalysisConfig _opinionAnalysisConfig;
        public SentimentAnalysisService(ArticleHelper articleHelper,
            IOptions<OpinionAnalysisConfig> opinionAnalysisConfig, IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _articleHelper = articleHelper;
            _opinionAnalysisConfig = opinionAnalysisConfig.Value;
        }

        public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode)
        {
            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                Positive = g.Sum(A => A.sentiment_count.Positive),
                Negative = g.Sum(A => A.sentiment_count.Negative),
            }).ToList();


            var dayRange = dateRange;
            var posCount = 0;
            var negCount = 0;
            var leftDate = startDate.AddDays(dayRange);
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var postiveNumber = new List<int>();
            var negtiveNumber = new List<int>();

            var posHotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();
            var posRedisHotArticles = new Dictionary<DateOnly, ICollection<string>>();
            var posRedisHotNewsArticles = new Dictionary<DateOnly, ICollection<string>>();

            var negHotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();
            var negRedisHotArticles = new Dictionary<DateOnly, ICollection<string>>();
            var negRedisHotNewsArticles = new Dictionary<DateOnly, ICollection<string>>();

            var wordAnalysisResults = new List<WordCloudAnalysisResult>();
            var wordCloudService = ServiceProvider.GetRequiredService<IWordCloudService>();


            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                #region 正向統計

                posCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Positive);
                postiveNumber.Add(posCount);

                var currentPosHotArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate && g.sentiment_count.Positive >= g.sentiment_count.Negative)
                    .OrderByDescending(o => o.sentiment_count!.Positive).Take(1)
                    .ToList();

                var currentPosHotNewsArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate && g.sentiment_count.Positive >= g.sentiment_count.Negative && g.ArticleTitle.Contains("[新聞"))
                    .OrderByDescending(o => o.sentiment_count!.Positive).Take(1)
                    .ToList();

                posHotArticles.TryAdd(leftDate, currentPosHotArticles.Select(o => o.ToAtricleUserView()).ToList());
                posRedisHotArticles.TryAdd(leftDate, currentPosHotArticles.Select(o => o.Content).ToList()!);
                posRedisHotNewsArticles.TryAdd(leftDate, currentPosHotNewsArticles.Select(o => o.Content).ToList()!);

                #endregion
                #region 負向統計
                negCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Negative);
                negtiveNumber.Add(negCount);

                var currentNegHotArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate && g.sentiment_count.Negative >= g.sentiment_count.Positive)
                    .OrderByDescending(o => o.sentiment_count!.Negative).Take(1)
                    .ToList();
                var currentNegHotNewsArticles = article
                    .Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate && g.sentiment_count.Negative >= g.sentiment_count.Positive && g.ArticleTitle.Contains("[新聞"))
                    .OrderByDescending(o => o.sentiment_count!.Negative).Take(1)
                    .ToList();

                negHotArticles.TryAdd(leftDate, currentNegHotArticles.Select(o => o.ToAtricleUserView()).ToList());
                negRedisHotArticles.TryAdd(leftDate, currentNegHotArticles.Select(o => o.Content).ToList()!);
                negRedisHotNewsArticles.TryAdd(leftDate, currentNegHotNewsArticles.Select(o => o.Content).ToList()!);

                #endregion
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

            await redisDatabase.HashSetAsync(redisKey, "Positive", JsonSerializer.Serialize(posRedisHotArticles), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "Negative", JsonSerializer.Serialize(negRedisHotArticles), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "PositiveNews", JsonSerializer.Serialize(posRedisHotNewsArticles), When.NotExists);
            await redisDatabase.HashSetAsync(redisKey, "NegativeNews", JsonSerializer.Serialize(negRedisHotNewsArticles), When.NotExists);

            redisDatabase.KeyExpire(redisKey, TimeSpan.FromSeconds(_opinionAnalysisConfig.RedisExpireSecond));

            var sentimentAnalysisResponse = new SentimentAnalysisResponse(
                    PositiveNumber: postiveNumber,
                    NegativeNumber: negtiveNumber,
                    Dates: dateOfAnalysis,
                    NegHotArticle: negHotArticles,
                    PosHotArticle: posHotArticles,
                    WordCloudAnalysisResults: wordAnalysisResults
                );
            return sentimentAnalysisResponse;
        }

    }
}
