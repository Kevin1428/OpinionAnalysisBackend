using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.WordCloud
{
    public class WordCloudService : IWordCloudService
    {
        //public async Task<WordCloudResponse> GetWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        //{
        //    Dictionary<string, int> keywordCount = new Dictionary<string, int>();
        //    keywordCount = await RoughAnalysis(topic, startDate, endDate, keywordCount);

        //    var sortedKeywordCount = keywordCount.OrderByDescending(pair => pair.Value).Take(20).ToList();
        //    WordCloudResponse response = new WordCloudResponse(
        //        wordSegment: sortedKeywordCount.Select(d => d.Key).ToList(),
        //        frequency: sortedKeywordCount.Select(d => d.Value).ToList());

        //    return response;

        //}
        private LinQArticleHelper LinQArticleHelper;

        public WordCloudService(LinQArticleHelper linQArticleHelper)
        {
            LinQArticleHelper = linQArticleHelper;
        }

        public async Task<WordCloudResponse> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            var articles = LinQArticleHelper.Articles;
            var article = LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate);

            var content = articles.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(20);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));

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
            if (!keyword.Equals(""))
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

        public async Task<WordCloudResponse> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            var articles = LinQArticleHelper.Articles;
            var article = LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate).Where(a => a.ContentSentiment.Equals("positive"));

            var content = articles.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.Where(M => M.PushContentSentiment.Equals("positive")).SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(20);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));
        }

        public async Task<WordCloudResponse> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            var articles = LinQArticleHelper.Articles;
            var article = LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate).Where(a => a.ContentSentiment.Equals("negative"));

            var content = articles.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.Where(M => M.PushContentSentiment.Equals("negative")).SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(20);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));
        }
    }

}
