using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public class PopularityAnalysisService : IPopularityAnalysisService
    {

        private LinQArticleHelper LinQArticleHelper;

        public PopularityAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            LinQArticleHelper = linQArticleHelper;
        }

        public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {

            var article = await LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                count = g.Sum(A => A.MessageCount!.All)
            }).ToList();


            var dayRange = 30;
            var disCount = 0;
            var leftDate = startDate;
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var discussNumber = new List<int>();

            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);
                disCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.count);

                discussNumber.Add(disCount);

                leftDate = rightDate;
                rightDate = rightDate.AddDays(dayRange);
            }

            return await Task.FromResult(new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                                        Dates: dateOfAnalysis));
        }

    }
}

