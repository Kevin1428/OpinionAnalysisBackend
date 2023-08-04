using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis;

namespace GraduationProjectBackend.Services.OpinionAnalysis.PopularityAnalysis
{
    public interface IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);
    }
}
