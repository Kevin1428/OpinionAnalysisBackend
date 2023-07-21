using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader;

namespace GraduationProjectBackend.Services.WordCloud
{
    public class WordCloudService : IWordCloudService
    {
        public async Task<WordCloudResponse> GetWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            Dictionary<string, int> keywordCount = new Dictionary<string, int>();

            await foreach (var article in ArticleHelper.GetArticlesAsync())
            {
                if (article.SearchDate < startDate || article.SearchDate > endDate)
                    continue;
                if (article.ArticleTitle?.Contains(topic) != true || article.Content?.Contains(topic) != true)
                    continue;

                foreach (var keyword in article.ProcessedArticleTitle!)
                {
                    AddKeywordCount(keyword, ref keywordCount);
                }

                foreach (var keyword in article.ProcessedContent!)
                {
                    AddKeywordCount(keyword, ref keywordCount);
                }
                foreach (var message in article.Messages!)
                {
                    foreach (var keyword in message.ProcessedPushContent!)
                    {
                        AddKeywordCount(keyword, ref keywordCount);
                    }
                }
            }

            var sortedKeywordCount = keywordCount.OrderByDescending(pair => pair.Value).Take(20).ToList();
            WordCloudResponse response = new WordCloudResponse(
                wordSegment: sortedKeywordCount.Select(d => d.Key).ToList(),
                frequency: sortedKeywordCount.Select(d => d.Value).ToList());

            return response;

        }

        private void AddKeywordCount(string keyword, ref Dictionary<string, int> keywordCount)
        {
            if (!keywordCount.TryAdd(keyword, 1))
            {
                keywordCount[keyword]++;
            }
        }
    }

}
