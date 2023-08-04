using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.OpinionAnalysis.WordCloud
{
    public class WordCloudService : IWordCloudService
    {

        private LinQArticleHelper LinQArticleHelper;

        public WordCloudService(LinQArticleHelper linQArticleHelper)
        {
            LinQArticleHelper = linQArticleHelper;
        }

        public async Task<WordCloudResponse> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            var article = await LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch);

            var content = article.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(50);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));

        }

        public async Task<WordCloudResponse> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            var article = (await LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch)).Where(a => a.ContentSentiment.Equals("positive"));

            var content = article.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.Where(M => M.PushContentSentiment.Equals("positive")).SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(50);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));
        }

        public async Task<WordCloudResponse> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            var article = (await LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch)).Where(a => a.ContentSentiment.Equals("negative"));

            var content = article.SelectMany(A => A.ProcessedContent).ToList();
            var pushContent = article.SelectMany(A => A.Messages.Where(M => M.PushContentSentiment.Equals("negative")).SelectMany(M => M.ProcessedPushContent).ToList()).ToList();

            content.AddRange(pushContent);

            var dic = content.GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count()).OrderByDescending(D => D.Value).Take(50);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            return await Task.FromResult(new WordCloudResponse(wordSegment, frequency));
        }
    }

}
