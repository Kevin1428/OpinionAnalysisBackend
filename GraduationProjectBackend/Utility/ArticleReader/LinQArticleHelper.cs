using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
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
            var files = Directory.EnumerateFiles(filePath, "*.json");

            foreach (var file in files)
            {
                FileStream fs = new FileStream(file, FileMode.Open);

                ArticleMapRoot? articleMapRoot = await JsonSerializer.DeserializeAsync<ArticleMapRoot>(fs);

                Articles.AddRange(articleMapRoot!.Articles!);
            }
        }

        public List<Article> GetArticlesInDateRange(string topic, DateOnly startDate, DateOnly endDate)
        {
            return Articles.Where(A => A.SearchDate >= startDate
                                              && A.SearchDate <= endDate
                                              && (A.ArticleTitle!.Contains(topic) || A.Content?.Contains(topic) == true)).ToList();

        }
    }
}

