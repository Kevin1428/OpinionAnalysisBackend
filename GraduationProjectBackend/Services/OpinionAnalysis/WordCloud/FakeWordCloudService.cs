using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;

namespace GraduationProjectBackend.Services.OpinionAnalysis.WordCloud
{
    public class FakeWordCloudService : IWordCloudService
    {
        public Task<WordCloudResponse> GetFullWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            WordCloudResponse response = new(
                wordSegment: new List<string>(){
                    "你好", "世界", "大家", "中文", "學習",
                    "開心", "好的", "感謝", "美麗", "夢想",
                    "成就", "努力", "快樂", "希望", "成功",
                    "明天", "一起", "健康", "友誼", "家庭"
                },
                frequency: new List<int>(){
                    180, 50, 90, 200, 30,
                    160, 70, 120, 190, 40,
                    150, 80, 110, 170, 200,
                    100, 60, 140, 200, 20
                })
            ;
            return Task.FromResult(response);
        }

        public Task<WordCloudResponse> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            throw new NotImplementedException();
        }

        public Task<WordCloudResponse> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            throw new NotImplementedException();
        }
    }
}

