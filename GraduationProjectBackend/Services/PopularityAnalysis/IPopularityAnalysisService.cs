using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using System;
using System.Threading.Tasks;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public interface IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly StartDate, DateOnly EndDate);
    }
}
