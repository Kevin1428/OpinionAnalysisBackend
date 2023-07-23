using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using System.Text.Json;

namespace GraduationProjectBackend.Utility.ArticleReader
{

    public class LinQArticleHelper
    {
        public List<Article> Articles { get; set; } = new List<Article>();

        public LinQArticleHelper()
        {
            string filePath = Directory.GetCurrentDirectory() + @"/Utility/ArticleReader/AtricleDates/";
            string[] files = Directory.GetFiles(filePath, "*.json");

            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                string articleJson = File.ReadAllText(file);
                ArticleMapRoot articleMapRoot = JsonSerializer.Deserialize<ArticleMapRoot>(articleJson)!;

                Articles.AddRange(articleMapRoot.Articles!);
            }
        }
    }
}

