﻿using GraduationProjectBackend.ConfigModel;
using GraduationProjectBackend.Enums;
using GraduationProjectBackend.Helper.Redis;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Microsoft.Extensions.Options;
using Nest;
using System.Text.Json;

namespace GraduationProjectBackend.Utility.ArticleReader
{

    public class ArticleHelper
    {
        private readonly OpinionAnalysisConfig _opinionAnalysisConfig;
        public ArticleHelper(IOptions<OpinionAnalysisConfig> opinionAnalysisConfig)
        {
            _opinionAnalysisConfig = opinionAnalysisConfig.Value;
        }

        public async Task<List<Article>> GetArticlesInDateRange(string topic, DateOnly startDate, DateOnly endDate,
            int dateRange, bool? isExactMatch, IEnumerable<AddressType>? addressTypes)
        {
            var redisDb = RedisHelper.GetRedisDatabase();
            var dataKey = topic + startDate.ToString() + endDate.ToString() + dateRange.ToString() + isExactMatch?.ToString();
            var articlesCache = await redisDb.HashGetAsync(dataKey, "ElsData");


            if (articlesCache.IsNull || true)
            {
                var articles = await GetElsDataAsync(topic, startDate, endDate, isExactMatch, addressTypes);
                //await redisDb.HashSetAsync(dataKey, new HashEntry[]
                //{
                //    new HashEntry("ElsData",JsonSerializer.Serialize(articles))
                //});
                //redisDb.KeyExpire(dataKey, TimeSpan.FromSeconds(60));


                return articles;
            }
            else
            {
                return JsonSerializer.Deserialize<List<Article>>(articlesCache!)!;
            }
        }
        private async Task<List<Article>> GetElsDataAsync(string topic, DateOnly startDate, DateOnly endDate,
            bool? isExactMatch, IEnumerable<AddressType>? addressTypes)
        {
            var node = new Uri("http://elasticsearch:9200");
            var settings = new ConnectionSettings(node).EnableApiVersioningHeader();
            var client = new ElasticClient(settings);

            var searchResult = new List<Article>();
            var hits = new List<IHit<Article>>();

            var indexName = "articles";

            var scrollTimeout = "1m"; // 設定 scroll 的逾時時間
            var scrollSize = _opinionAnalysisConfig.ElasticScrollSize; // 每次 scroll 擷取的文件數量

            ISearchResponse<Article> searchResponse;

            if (isExactMatch.HasValue && !isExactMatch.Value)
            {
                searchResponse = await client.SearchAsync<Article>(s => s
                    .Index(indexName)
                    .Query(q => q
                        .Bool(b => b
                            .Must(m => m
                                    .Bool(bb => bb
                                        .Should(
                                            sh => sh.Match(ma => ma.Field("articleTitle").Query(topic).MinimumShouldMatch(_opinionAnalysisConfig.ElasticSearchArticleTiTleMinimumShouldMatch)),
                                            sh => sh.Match(ma => ma.Field("content").Query(topic).MinimumShouldMatch(_opinionAnalysisConfig.ElasticSearchArticleContentMinimumShouldMatch))
                                            ).MinimumShouldMatch(2)
                                    ),
                                m => m
                                    .DateRange(r => r
                                        .Field("searchDate")
                                        .GreaterThanOrEquals(startDate.ToString("yyyy/MM/dd"))
                                        .LessThanOrEquals(endDate.ToString("yyyy/MM/dd"))
                                    )
                            )
                        )
                    ).Size(scrollSize).Scroll(scrollTimeout));
            }
            else
            {
                searchResponse = await client.SearchAsync<Article>(s => s
                   .Index(indexName)
                   .Query(q => q
                       .Bool(b => b
                           .Must(m => m
                                   .Bool(bb => bb
                                       .Should(
                                           sh => sh.MatchPhrase(ma => ma.Field("articleTitle").Query(topic)),
                                           sh => sh.MatchPhrase(ma => ma.Field("content").Query(topic))
                                           ).MinimumShouldMatch(2)
                                   ),
                               m => m
                                   .DateRange(r => r
                                       .Field("searchDate")
                                       .GreaterThanOrEquals(startDate.ToString("yyyy/MM/dd"))
                                       .LessThanOrEquals(endDate.ToString("yyyy/MM/dd"))
                                   )
                           )
                       )
                   ).Size(scrollSize).Scroll(scrollTimeout));
            }

            while (searchResponse.IsValid && !string.IsNullOrEmpty(searchResponse.ScrollId) && searchResponse.Documents.Any())
            {
                hits.AddRange(searchResponse.Hits);
                searchResponse = await client.ScrollAsync<Article>(scrollTimeout, searchResponse.ScrollId);
            }
            hits = hits.OrderByDescending(o => o.Score).Take((int)Math.Floor(hits.Count() * _opinionAnalysisConfig.ElasticSearchScorePercentage)).ToList();
            searchResult.AddRange(hits.Select(o => o.Source));
            var removeList = _opinionAnalysisConfig.RemoveTagWord;

            if (addressTypes is not null)
            {
                foreach (var article in searchResult)
                {
                    article.Messages = article.Messages.Where(o => addressTypes.Any(x => x == o.AddressType)).ToList();
                }
                searchResult = searchResult.Where(o => o.Messages.Count > 0 || addressTypes.Any(x => x == o.AddressType)).ToList();
            }

            return searchResult.OrderBy(article => DateOnly.Parse(article.SearchDate)).ToList();
        }
    }
}

