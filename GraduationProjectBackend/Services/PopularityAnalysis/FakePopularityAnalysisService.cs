using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public class FakePopularityAnalysisService : IPopularityAnalysisService
    {
        public Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange)
        {
            ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
            ICollection<int> discussNumber = new List<int>();
            var randomDiscussNumber = new Random();

            for (; endDate > startDate; endDate = endDate.AddDays(-7))
            {
                dateOfAnalysis.Add(endDate);
                discussNumber.Add(randomDiscussNumber.Next(1, 10000));
            }

            var PopularityAnalysisResponse = new PopularityAnalysisResponse(
                    DiscussNumber: discussNumber,
                    Dates: dateOfAnalysis
                );
            return Task.FromResult(PopularityAnalysisResponse);
        }
    }
}
