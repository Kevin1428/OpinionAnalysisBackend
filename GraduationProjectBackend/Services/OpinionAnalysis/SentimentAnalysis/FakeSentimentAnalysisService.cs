using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis;

namespace GraduationProjectBackend.Services.OpinionAnalysis.SentimentAnalysis
{
    public class FakeSentimentAnalysisService : ISentimentAnalysisService
    {
        public Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
            ICollection<int> postiveNumber = new List<int>();
            ICollection<int> negtiveNumber = new List<int>();
            var randomDiscussNumber = new Random();

            for (; endDate > startDate; endDate = endDate.AddDays(-7))
            {
                dateOfAnalysis.Add(endDate);
                postiveNumber.Add(randomDiscussNumber.Next(1, 10000));
                negtiveNumber.Add(randomDiscussNumber.Next(1, 10000));
            }

            var PopularityAnalysisResponse = new SentimentAnalysisResponse(
                    PositiveNumber: postiveNumber,
                    NegativeNumber: negtiveNumber,
                    Dates: dateOfAnalysis
                );
            return Task.FromResult(PopularityAnalysisResponse);
        }
    }
}
