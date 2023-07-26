using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public class PopularityAnalysisService : IPopularityAnalysisService
    {
        //public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        //{
        //    ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
        //    ICollection<int> discussNumber = new List<int>();

        //    int discussCount = 0;

        //    byte rangeWidth = 3;
        //    DateOnly currentRangeEndDate = startDate.AddDays(rangeWidth - 1);

        //    bool stopFlag = false;
        //    if (currentRangeEndDate >= endDate)
        //    {
        //        stopFlag = true;
        //        currentRangeEndDate = endDate;
        //    }

        //    Article? currentArticle = default;
        //    await foreach (var article in ArticleHelper.GetArticlesAsync())
        //    {
        //        while (article.SearchDate > currentRangeEndDate)
        //        {
        //            dateOfAnalysis.Add(currentRangeEndDate);
        //            discussNumber.Add(discussCount);

        //            if (stopFlag)
        //                break;

        //            discussCount = 0;


        //            startDate = currentRangeEndDate;
        //            currentRangeEndDate = startDate.AddDays(rangeWidth);

        //            if (currentRangeEndDate >= endDate)
        //            {
        //                stopFlag = true;
        //                currentRangeEndDate = endDate;
        //            }

        //        }

        //        if (article.SearchDate >= startDate
        //            && article.SearchDate <= currentRangeEndDate
        //            && (article.ArticleTitle!.Contains(topic) || article.Content.Contains(topic)))
        //        {
        //            discussCount += article.MessageCount!.All;
        //        }
        //        currentArticle = article;
        //    }

        //    if (currentArticle!.SearchDate <= currentRangeEndDate)
        //    {
        //        while (currentRangeEndDate < endDate)
        //        {

        //            dateOfAnalysis.Add(currentRangeEndDate);
        //            discussNumber.Add(discussCount);

        //            if (stopFlag)
        //                break;

        //            discussCount = 0;


        //            startDate = currentRangeEndDate;
        //            currentRangeEndDate = startDate.AddDays(rangeWidth);

        //            if (currentRangeEndDate >= endDate)
        //            {
        //                stopFlag = true;
        //                currentRangeEndDate = endDate;
        //            }
        //        }
        //    }

        //    var PopularityAnalysisResponse = new PopularityAnalysisResponse(DiscussNumber: discussNumber.Reverse().ToList(),
        //                                                                    Dates: dateOfAnalysis.Reverse().ToList());
        //    return PopularityAnalysisResponse;
        //}
        private LinQArticleHelper LinQArticleHelper;

        public PopularityAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            LinQArticleHelper = linQArticleHelper;
        }

        public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {

            var article = LinQArticleHelper.GetArticlesInDateRange(topic, startDate, endDate);

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = g.Key,
                count = g.Sum(A => A.MessageCount!.All)
            }).ToList();


            var DayRange = 7;
            var dayCount = 0;
            var disCount = 0;
            var currentDate = startDate;
            var dateOfAnalysis = new List<DateOnly>();
            var discussNumber = new List<int>();

            while (currentDate < groupByDayArticles[0].Date)
            {
                currentDate = FillZero(DayRange, currentDate, dateOfAnalysis, discussNumber);
            }

            foreach (var c in groupByDayArticles)
            {

                disCount += c.count;
                dayCount++;

                if (dayCount == DayRange || c == groupByDayArticles.Last())
                {
                    dateOfAnalysis.Add(c.Date);
                    discussNumber.Add(disCount);
                    disCount = 0;
                    dayCount = 0;
                }
                currentDate = currentDate.AddDays(DayRange);
            }

            while (currentDate <= endDate)
            {
                currentDate = FillZero(DayRange, currentDate, dateOfAnalysis, discussNumber);
            }
            return await Task.FromResult(new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                                        Dates: dateOfAnalysis));
        }

        private static DateOnly FillZero(int DayRange, DateOnly currentDate, List<DateOnly> dateOfAnalysis, List<int> discussNumber)
        {
            /*            dateOfAnalysis.Add(currentDate);
                        discussNumber.Add(0);*/
            currentDate = currentDate.AddDays(DayRange);
            return currentDate;
        }
    }
}

