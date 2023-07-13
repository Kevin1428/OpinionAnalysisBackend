using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;

namespace GraduationProjectBackend.Services.WordCloud
{
    public class FakeWordCloudService : IWordCloudService
    {
        public async Task<WordCloudResponse> GetWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate)
        {
            WordCloudResponse response = new()
            {
                WordSegment = new List<string>(){
                    "你好", "世界", "大家", "中文", "學習",
                    "開心", "好的", "感謝", "美麗", "夢想",
                    "成就", "努力", "快樂", "希望", "成功",
                    "明天", "一起", "健康", "友誼", "家庭"
                },
                Frequency = new List<int>(){
                    180, 50, 90, 200, 30,
                    160, 70, 120, 190, 40,
                    150, 80, 110, 170, 200,
                    100, 60, 140, 200, 20
                }
            };

            return response;
        }
    }
}

