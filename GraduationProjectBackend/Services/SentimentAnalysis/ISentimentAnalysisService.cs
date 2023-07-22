using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;

namespace GraduationProjectBackend.Services.SentimentAnalysis
{
    public interface ISentimentAnalysisService
    {
        public Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate);
    }
}
