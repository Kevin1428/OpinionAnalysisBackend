using GraduationProjectBackend.ConfigModel;
using GraduationProjectBackend.Querys.UserViews;
using GraduationProjectBackend.Services.OpinionAnalysis;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Microsoft.Extensions.Options;
using Nest;

namespace GraduationProjectBackend.Querys
{
    public class TrendingTopicQuery : QueryBase, ITrendingTopicQuery
    {
        public TrendingTopicQuery(IOptions<OpinionAnalysisConfig> opinionAnalysisConfig) : base(opinionAnalysisConfig) { }

        public async Task<TrendingTopicUserView> GetTrendingTopicAsync(OpinionAnalysisParam param)
        {
            var node = new Uri("http://elasticsearch:9200");
            var settings = new ConnectionSettings(node).EnableApiVersioningHeader();
            var client = new ElasticClient(settings);

            var searchResult = new List<Article>();
            var hits = new List<IHit<Article>>();
            var indexName = "articles";

            var scrollTimeout = "1m"; // 設定 scroll 的逾時時間
            var scrollSize = _opinionAnalysisConfig.ElasticScrollSize; // 每次 scroll 擷取的文件數量

            var searchResponse = await client.SearchAsync<Article>(s => s
                .Index(indexName)
                .Query(q => q
                    .Bool(b => b
                        .Must(m => m
                                .DateRange(r => r
                                    .Field("searchDate")
                                    .GreaterThanOrEquals(param.StartDate.ToString("yyyy/MM/dd"))
                                    .LessThanOrEquals(param.EndDate.ToString("yyyy/MM/dd"))
                             )
                        )
                    )
                ).Size(scrollSize).Scroll(scrollTimeout));

            while (searchResponse.IsValid && !string.IsNullOrEmpty(searchResponse.ScrollId) && searchResponse.Documents.Any())
            {
                hits.AddRange(searchResponse.Hits);
                searchResponse = await client.ScrollAsync<Article>(scrollTimeout, searchResponse.ScrollId);
            }

            hits = hits.OrderByDescending(o => o.Score).Take((int)Math.Floor(hits.Count() * _opinionAnalysisConfig.ElasticSearchScorePercentage)).ToList();
            searchResult.AddRange(hits.Select(o => o.Source));

            var removeList = _opinionAnalysisConfig.RemoveTagWord;
            searchResult = searchResult.OrderBy(article => DateOnly.Parse(article.SearchDate)).ToList();

            var nbWords = new List<string>();

            //nbWords.AddRange(searchResult.SelectMany(A => A.ProcessedNbContent));
            nbWords.AddRange(searchResult.SelectMany(A => A.ProcessedNbArticleTitle));
            //nbWords.AddRange(searchResult.SelectMany(A =>
            //A.Messages.SelectMany(M => M.ProcessedNbPushContent!)));

            var nbDic = nbWords.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(D => D.Value).Take(_opinionAnalysisConfig.WordCloudTakeNumber);

            var nbWordSegment = nbDic.Select(pair => pair.Key).ToList();
            var nbFrequency = nbWordSegment.Select(w => searchResult.Where(o => o.ArticleTitle.Contains(w) /*|| o.Content.Contains(w)*/).Count());
            var percentage = nbFrequency.Select(o => o / (float)searchResult.Count);

            var dtos = nbWordSegment.Zip(nbFrequency, percentage).Select(o => new TrendingTopicUserView.TrendingTopicDto(o.First, o.Second, o.Third)).OrderByDescending(o => o.DiscussPercentage);

            var result = new TrendingTopicUserView(
                ArticleNumber: searchResult.Count,
                FindPercentage: dtos.Sum(o => o.DiscussPercentage),
                TrendingTopics: dtos);

            return result;
        }
    }
}
