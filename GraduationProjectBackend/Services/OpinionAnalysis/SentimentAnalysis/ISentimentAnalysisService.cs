using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis;

namespace GraduationProjectBackend.Services.OpinionAnalysis.SentimentAnalysis
{
    public interface ISentimentAnalysisService
    {
        public Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);
    }
}
