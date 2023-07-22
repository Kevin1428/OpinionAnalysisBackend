using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.WordCloud
{
    public class WordCloudService : IWordCloudService
    {
        public async Task<WordCloudResponse> GetWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            Dictionary<string, int> keywordCount = new Dictionary<string, int>();
            keywordCount = await RoughAnalysis(topic, startDate, endDate, keywordCount);

            var sortedKeywordCount = keywordCount.OrderByDescending(pair => pair.Value).Take(20).ToList();
            WordCloudResponse response = new WordCloudResponse(
                wordSegment: sortedKeywordCount.Select(d => d.Key).ToList(),
                frequency: sortedKeywordCount.Select(d => d.Value).ToList());

            return response;

        }

        private async Task<Dictionary<string, int>> RoughAnalysis(string topic, DateOnly startDate, DateOnly endDate, Dictionary<string, int> keywordCount)
        {
            await foreach (var article in ArticleHelper.GetArticlesAsync())
            {
                if (article.SearchDate < startDate || article.SearchDate > endDate)
                    continue;
                if (article.ArticleTitle?.Contains(topic) == true || article.Content?.Contains(topic) == true)
                {
                    AddTitleAndContent(article, ref keywordCount);
                    AddMessageContent(article, ref keywordCount);
                }
                else
                {
                    AddOnlyMatchMessageContent(article, ref keywordCount, topic);
                }


            }

            return keywordCount;
        }

        private async Task<Dictionary<string, int>> DetailedAnalysis(string topic, DateOnly startDate, DateOnly endDate, Dictionary<string, int> keywordCount)
        {
            await foreach (var article in ArticleHelper.GetArticlesAsync())
            {
                if (article.SearchDate < startDate || article.SearchDate > endDate)
                    continue;

                if (article.ArticleTitle?.Contains(topic) == true || article.Content?.Contains(topic) == true)
                {
                    AddTitleAndContent(article, ref keywordCount);
                    AddOnlyMatchMessageContent(article, ref keywordCount, topic);
                }
                else
                {
                    AddOnlyMatchMessageContent(article, ref keywordCount, topic);
                }
            }

            return keywordCount;
        }

        private void AddKeywordCount(string keyword, ref Dictionary<string, int> keywordCount)
        {
            if (!keywordCount.TryAdd(keyword, 1))
            {
                keywordCount[keyword]++;
            }
        }

        private void AddTitleAndContent(Article article, ref Dictionary<string, int> keywordCount)
        {
            foreach (var keyword in article.ProcessedArticleTitle!)
            {
                AddKeywordCount(keyword, ref keywordCount);
            }

            foreach (var keyword in article.ProcessedContent!)
            {
                AddKeywordCount(keyword, ref keywordCount);
            }
        }

        private void AddMessageContent(Article article, ref Dictionary<string, int> keywordCount)
        {
            foreach (var message in article.Messages!)
            {
                foreach (var keyword in message.ProcessedPushContent!)
                {
                    AddKeywordCount(keyword, ref keywordCount);
                }
            }
        }
        private void AddOnlyMatchMessageContent(Article article, ref Dictionary<string, int> keywordCount, string topic)
        {
            foreach (var message in article.Messages!)
            {
                if (message.PushContent!.Contains(topic))
                    foreach (var keyword in message.ProcessedPushContent!)
                    {
                        AddKeywordCount(keyword, ref keywordCount);
                    }
            }
        }
    }

}
