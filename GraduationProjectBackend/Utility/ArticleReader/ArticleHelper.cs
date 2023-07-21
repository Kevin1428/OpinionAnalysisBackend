using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using System.Globalization;
using System.Text.Json;

namespace GraduationProjectBackend.Utility.ArticleReader
{
    public static class ArticleHelper
    {
        public static async IAsyncEnumerable<Article> GetArticlesAsync()
        {

            string filePath = Directory.GetCurrentDirectory() + @"/Utility/ArticleReader/AtricleDates/";
            string[] files = Directory.GetFiles(filePath, "*.json");

            foreach (string file in files)
            {
                string articleJson = await File.ReadAllTextAsync(file);
                ArticleMapRoot articleMapRoot = JsonSerializer.Deserialize<ArticleMapRoot>(articleJson)!;
                if (articleMapRoot != null)
                {
                    var articles = articleMapRoot.Articles;
                    string[] formats =
                    {
                        "ddd MMM d HH:mm:ss yyyy",
                        "ddd MMM dd HH:mm:ss yyyy"
                    };

                    foreach (var art in articles!)
                    {
                        DateTime.TryParseExact(art.StringDate,
                                               formats,
                                               CultureInfo.InvariantCulture,
                                               DateTimeStyles.None,
                                               out var parsedDate);

                        art.SearchDate = DateOnly.FromDateTime(parsedDate);
                        yield return art;
                    }
                }
            }
        }
    }
}

