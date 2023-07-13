using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using System;
using System.Threading.Tasks;

namespace GraduationProjectBackend.Services.SentimentAnalysis
{
    public interface ISentimentAnalysisService
    {
        public Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly StartDate, DateOnly EndDate);
    }
}
