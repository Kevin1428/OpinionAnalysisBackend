using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Nest;
using System.Text.Json;

namespace GraduationProjectBackend.Utility.ArticleReader
{

    public class LinQArticleHelper
    {
        public List<Article> Articles { get; set; } = new List<Article>();

        public LinQArticleHelper()
        {
        }

        public async Task LoadArticle()
        {
            string filePath = Directory.GetCurrentDirectory() + @"/Utility/ArticleReader/AtricleDates/";

            //var files = Directory.EnumerateFiles(filePath, "*.json");
            var files = new List<string>();

            foreach (var file in files)
            {
                FileStream fs = new FileStream(file, FileMode.Open);

                ArticleMapRoot? articleMapRoot = await JsonSerializer.DeserializeAsync<ArticleMapRoot>(fs);

                Articles.AddRange(articleMapRoot!.Articles!);
            }
        }

        public async Task<List<Article>> GetArticlesInDateRange(string topic, DateOnly startDate, DateOnly endDate)
        {
            var searchResult = new List<Article>();
            var node = new Uri("http://elasticsearch:9200");
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            var indexName = "articles";

            var scrollTimeout = "1m"; // 設定 scroll 的逾時時間
            var scrollSize = 5000; // 每次 scroll 擷取的文件數量

            var searchResponse = await client.SearchAsync<Article>(s => s
                                        .Index("articles")
                                        .Query(q => q
                                            .Bool(b => b
                                                .Must(m => m
                                                    .Bool(bb => bb
                                                        .Should(
                                                            sh => sh.Match(ma => ma.Field("article_title").Query(topic)),
                                                            sh => sh.Match(ma => ma.Field("content").Query(topic))
                                                        )
                                                        .MinimumShouldMatch(1)
                                                    ),
                                                    m => m
                                                    .DateRange(r => r
                                                        .Field("searchDate")
                                                        .GreaterThanOrEquals(startDate.ToString("yyyy/MM/dd"))
                                                        .LessThanOrEquals(endDate.ToString("yyyy/MM/dd"))
                                                    )
                                                )
                                            )
                                          ).Size(5000)
                                    .Scroll(scrollTimeout));

            while (searchResponse.IsValid && !string.IsNullOrEmpty(searchResponse.ScrollId) && searchResponse.Documents.Any())
            {
                searchResponse = await client.ScrollAsync<Article>(scrollTimeout, searchResponse.ScrollId);

                if (searchResponse.IsValid && searchResponse.Documents.Any())
                {

                    searchResult.AddRange(searchResponse.Documents);

                }
            }
            return (searchResult.OrderBy(article => DateOnly.Parse(article.SearchDate)).ToList());

        }
    }
}

