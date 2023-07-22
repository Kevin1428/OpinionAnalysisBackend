using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.SentimentAnalysis
{
    public class SentimentAnalysisService : ISentimentAnalysisService
    {
        public async Task<SentimentAnalysisResponse> GetSentimentAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {
            ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
            ICollection<int> postiveNumber = new List<int>();
            ICollection<int> negtiveNumber = new List<int>();


            int postiveCount = 0;
            int negtiveCount = 0;

            byte rangeWidth = 7;
            DateOnly currentRangeEndDate = startDate.AddDays(rangeWidth - 1);

            bool stopFlag = false;
            if (currentRangeEndDate >= endDate)
            {
                stopFlag = true;
                currentRangeEndDate = endDate;
            }

            Article? currentArticle = default;
            await foreach (var article in ArticleHelper.GetArticlesAsync())
            {
                while (article.SearchDate > currentRangeEndDate)
                {
                    dateOfAnalysis.Add(currentRangeEndDate);
                    postiveNumber.Add(postiveCount);
                    negtiveNumber.Add(negtiveCount);

                    if (stopFlag)
                        break;

                    postiveCount = 0;
                    negtiveCount = 0;

                    startDate = currentRangeEndDate;
                    currentRangeEndDate = startDate.AddDays(rangeWidth);

                    if (currentRangeEndDate >= endDate)
                    {
                        stopFlag = true;
                        currentRangeEndDate = endDate;
                    }

                }

                if (article.SearchDate >= startDate
                    && article.SearchDate <= currentRangeEndDate
                    && (article.ArticleTitle!.Contains(topic) || article.Content.Contains(topic)))
                {
                    postiveCount += article.sentiment_count!.Positive;
                    negtiveCount += article.sentiment_count!.Negative;

                }
                currentArticle = article;
            }

            if (currentArticle!.SearchDate <= currentRangeEndDate)
            {
                while (currentRangeEndDate < endDate)
                {

                    dateOfAnalysis.Add(currentRangeEndDate);
                    postiveNumber.Add(postiveCount);
                    negtiveNumber.Add(negtiveCount);

                    if (stopFlag)
                        break;

                    postiveCount = 0;
                    negtiveCount = 0;

                    startDate = currentRangeEndDate;
                    currentRangeEndDate = startDate.AddDays(rangeWidth);

                    if (currentRangeEndDate >= endDate)
                    {
                        stopFlag = true;
                        currentRangeEndDate = endDate;
                    }
                }
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
