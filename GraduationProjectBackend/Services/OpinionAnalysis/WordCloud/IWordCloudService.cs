using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.OpinionAnalysis.WordCloud
{
    public interface IWordCloudService
    {
        public Task<WordCloudAnalysisResult> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);
        public Task<WordCloudAnalysisResult> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);

        public Task<WordCloudAnalysisResult> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);
        Task<WordCloudAnalysisResult> GetWordCloudResponse(List<Article> article, Func<Article, bool> contentFilter, Func<Article, bool> articleTitleFilter, Func<Message, bool> messagesContentFilter);
    }
}
