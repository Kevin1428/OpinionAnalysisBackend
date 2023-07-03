using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;

namespace GraduationProjectBackend.Services.WordCloud
{
    public interface IWordCloudService
    {
        public Task<WordCloudResponseDTO> GetWordCloudResponseDTO(string topic);
    }
}
