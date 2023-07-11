using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public interface IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly StartDate, DateOnly EndDate);


    }
}
