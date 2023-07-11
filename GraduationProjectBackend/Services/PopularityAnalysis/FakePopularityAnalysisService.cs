using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public class FakePopularityAnalysisService : IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {
            ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
            ICollection<int> DiscussNumber = new List<int>();

            for (; endDate > startDate; endDate = endDate.AddDays(-7))
            {
                dateOfAnalysis.Add(endDate);
            }

            var PopularityAnalysisResponse = new PopularityAnalysisResponse(
                    DiscussNumber: DiscussNumber,
                    Dates: dateOfAnalysis
                );
            return Task.FromResult<PopularityAnalysisResponse>(PopularityAnalysisResponse);
        }
    }
}
