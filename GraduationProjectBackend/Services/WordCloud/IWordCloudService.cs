using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;

namespace GraduationProjectBackend.Services.WordCloud
{
    public interface IWordCloudService
    {
        public Task<WordCloudResponse> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate);
        public Task<WordCloudResponse> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate);

        public Task<WordCloudResponse> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate);

    }
}
