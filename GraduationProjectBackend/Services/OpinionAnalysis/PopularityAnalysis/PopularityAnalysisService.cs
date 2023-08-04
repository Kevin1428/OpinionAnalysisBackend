using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.OpinionAnalysis.PopularityAnalysis
{
    public class PopularityAnalysisService : IPopularityAnalysisService
    {

        private LinQArticleHelper LinQArticleHelper;

        public PopularityAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            LinQArticleHelper = linQArticleHelper;
        }

        public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {

            var article = await LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                count = g.Sum(A => A.MessageCount!.All)
            }).ToList();


            var dayRange = dateRange;
            var disCount = 0;
            var leftDate = startDate;
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var discussNumber = new List<int>();
            var hotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();

            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                hotArticles.TryAdd(leftDate, article.Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate).OrderByDescending(o => o.MessageCount!.All).Select(o => o.ToAtricleUserView()).Take(1).ToList());

                disCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.count);

                discussNumber.Add(disCount);

                leftDate = rightDate;
                rightDate = rightDate.AddDays(dayRange);

            }

            return new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                  Dates: dateOfAnalysis,
                                                  HotArticles: hotArticles);
        }

    }
}

