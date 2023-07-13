using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;

namespace GraduationProjectBackend.Services.WordCloud
{
    public interface IWordCloudService
    {
        public Task<WordCloudResponse> GetWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate);
    }
}
