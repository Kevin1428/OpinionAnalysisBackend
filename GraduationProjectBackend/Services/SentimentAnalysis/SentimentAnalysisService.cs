using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;

namespace GraduationProjectBackend.Services.SentimentAnalysis
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private LinQArticleHelper linQArticleHelper;

        public SentimentAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            this.linQArticleHelper = linQArticleHelper;
        }

        public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange)
        {
            var article = await linQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                Positive = g.Sum(A => A.sentiment_count.Positive),
                Negative = g.Sum(A => A.sentiment_count.Negative)
            }).ToList();


            var dayRange = dateRange;
            var posCount = 0;
            var negCount = 0;
            var leftDate = startDate;
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var postiveNumber = new List<int>();
            var negtiveNumber = new List<int>();


            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                posCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Positive);
                postiveNumber.Add(posCount);

                negCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Negative);
                negtiveNumber.Add(negCount);

                leftDate = rightDate;
                rightDate = rightDate.AddDays(dayRange);
            }

            var sentimentAnalysisResponse = new SentimentAnalysisResponse(
                    PositiveNumber: postiveNumber,
                    NegativeNumber: negtiveNumber,
                    Dates: dateOfAnalysis
                );
            return sentimentAnalysisResponse;
        }

    }
}
