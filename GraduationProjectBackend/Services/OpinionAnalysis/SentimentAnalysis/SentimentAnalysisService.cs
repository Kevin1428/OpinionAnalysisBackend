using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.OpinionAnalysis.SentimentAnalysis
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        private LinQArticleHelper linQArticleHelper;

        public SentimentAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            this.linQArticleHelper = linQArticleHelper;
        }

        public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate, int dateRange, bool? isExactMatch)
        {
            var article = await linQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = DateOnly.Parse(g.Key),
                Positive = g.Sum(A => A.sentiment_count.Positive),
                Negative = g.Sum(A => A.sentiment_count.Negative),
            }).ToList();


            var dayRange = dateRange;
            var posCount = 0;
            var negCount = 0;
            var leftDate = startDate;
            var rightDate = leftDate.AddDays(dayRange);
            var dateOfAnalysis = new List<DateOnly>();
            var postiveNumber = new List<int>();
            var negtiveNumber = new List<int>();

            var posHotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();
            var negHotArticles = new Dictionary<DateOnly, ICollection<ArticleUserView>>();

            while (leftDate < endDate)
            {
                dateOfAnalysis.Add(leftDate);

                posCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Positive);
                postiveNumber.Add(posCount);
                posHotArticles.TryAdd(leftDate, article.Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate).OrderByDescending(o => o.sentiment_count!.Positive).Select(o => o.ToAtricleUserView()).Take(1).ToList());


                negCount = groupByDayArticles.Where(g => g.Date >= leftDate && g.Date <= rightDate).Sum(g => g.Negative);
                negtiveNumber.Add(negCount);
                negHotArticles.TryAdd(leftDate, article.Where(g => DateOnly.Parse(g.SearchDate) > leftDate && DateOnly.Parse(g.SearchDate) <= rightDate).OrderByDescending(o => o.sentiment_count!.Negative).Select(o => o.ToAtricleUserView()).Take(1).ToList());


                leftDate = rightDate;
                rightDate = rightDate.AddDays(dayRange);
            }

            var sentimentAnalysisResponse = new SentimentAnalysisResponse(
                    PositiveNumber: postiveNumber,
                    NegativeNumber: negtiveNumber,
                    Dates: dateOfAnalysis,
                    NegHotArticle: negHotArticles,
                    PosHotArticle: posHotArticles
                );
            return sentimentAnalysisResponse;
        }

    }
}
