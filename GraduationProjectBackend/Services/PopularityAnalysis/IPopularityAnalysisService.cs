using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public interface IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange);
    }
}
