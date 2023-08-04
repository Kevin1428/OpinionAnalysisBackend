using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;

namespace GraduationProjectBackend.Services.OpinionAnalysis.WordCloud
{
    public interface IWordCloudService
    {
        public Task<WordCloudResponse> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);
        public Task<WordCloudResponse> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);

        public Task<WordCloudResponse> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch);

    }
}
