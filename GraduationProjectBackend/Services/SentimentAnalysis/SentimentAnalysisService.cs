using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;

namespace GraduationProjectBackend.Services.SentimentAnalysis
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        //public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        //{
        //    ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
        //    ICollection<int> postiveNumber = new List<int>();
        //    ICollection<int> negtiveNumber = new List<int>();


        //    int postiveCount = 0;
        //    int negtiveCount = 0;

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
        //            postiveNumber.Add(postiveCount);
        //            negtiveNumber.Add(negtiveCount);

        //            if (stopFlag)
        //                break;

        //            postiveCount = 0;
        //            negtiveCount = 0;

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
        //            postiveCount += article.sentiment_count!.Positive;
        //            negtiveCount += article.sentiment_count!.Negative;

        //        }
        //        currentArticle = article;
        //    }

        //    if (currentArticle!.SearchDate <= currentRangeEndDate)
        //    {
        //        while (currentRangeEndDate < endDate)
        //        {

        //            dateOfAnalysis.Add(currentRangeEndDate);
        //            postiveNumber.Add(postiveCount);
        //            negtiveNumber.Add(negtiveCount);

        //            if (stopFlag)
        //                break;

        //            postiveCount = 0;
        //            negtiveCount = 0;

        //            startDate = currentRangeEndDate;
        //            currentRangeEndDate = startDate.AddDays(rangeWidth);

        //            if (currentRangeEndDate >= endDate)
        //            {
        //                stopFlag = true;
        //                currentRangeEndDate = endDate;
        //            }
        //        }
        //    }

        //    var sentimentAnalysisResponse = new SentimentAnalysisResponse(
        //            PositiveNumber: postiveNumber.Reverse().ToList(),
        //            NegativeNumber: negtiveNumber.Reverse().ToList(),
        //            Dates: dateOfAnalysis.Reverse().ToList()
        //        );
        //    return sentimentAnalysisResponse;
        //}

        private LinQArticleHelper linQArticleHelper;

        public SentimentAnalysisService(LinQArticleHelper linQArticleHelper)
        {
            this.linQArticleHelper = linQArticleHelper;
        }

        public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {
            var articles = linQArticleHelper.Articles;
            var article = articles.Where(A => A.SearchDate >= startDate
                                              && A.SearchDate <= endDate
                                              && (A.ArticleTitle!.Contains(topic) || A.Content?.Contains(topic) == true)).ToList();

            var groupByDayArticles = article.GroupBy(a => a.SearchDate).Select(g => new
            {
                Date = g.Key,
                Positive = g.Sum(A => A.sentiment_count.Positive),
                Negative = g.Sum(A => A.sentiment_count.Negative)
            }).ToList();


            var DayRange = 7;
            var dayCount = 0;
            var posCount = 0;
            var negCount = 0;
            var currentDate = startDate;
            var dateOfAnalysis = new List<DateOnly>();
            var postiveNumber = new List<int>();
            var negtiveNumber = new List<int>();


            while (currentDate < groupByDayArticles[0].Date)
            {
                currentDate = FillZero(DayRange, currentDate, dateOfAnalysis, postiveNumber, negtiveNumber);
            }

            foreach (var c in groupByDayArticles)
            {

                posCount += c.Positive;
                negCount += c.Negative;
                dayCount++;

                if (dayCount == DayRange || c == groupByDayArticles.Last())
                {
                    dateOfAnalysis.Add(c.Date);
                    postiveNumber.Add(posCount);
                    negtiveNumber.Add(negCount);

                    posCount = 0;
                    negCount = 0;
                    dayCount = 0;
                }
                currentDate = currentDate.AddDays(DayRange);
            }

            while (currentDate <= endDate)
            {
                currentDate = FillZero(DayRange, currentDate, dateOfAnalysis, postiveNumber, negtiveNumber);
            }


            var sentimentAnalysisResponse = new SentimentAnalysisResponse(
                    PositiveNumber: postiveNumber,
                    NegativeNumber: negtiveNumber,
                    Dates: dateOfAnalysis
                );
            return sentimentAnalysisResponse;
        }

        private static DateOnly FillZero(int DayRange, DateOnly currentDate, List<DateOnly> dateOfAnalysis, List<int> posNumber, List<int> negNumber)
        {
            dateOfAnalysis.Add(currentDate);
            posNumber.Add(0);
            negNumber.Add(0);
            currentDate = currentDate.AddDays(DayRange);
            return currentDate;
        }
    }
}
