using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.Services.PopularityAnalysis
{
    public class PopularityAnalysisService : IPopularityAnalysisService
    {
        public async Task<PopularityAnalysisResponse> GetPopularityAnalysisResponse(string topic, DateOnly startDate, DateOnly endDate)
        {
            ICollection<DateOnly> dateOfAnalysis = new List<DateOnly>();
            ICollection<int> discussNumber = new List<int>();

            int discussCount = 0;

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
                    discussNumber.Add(discussCount);

                    if (stopFlag)
                        break;

                    discussCount = 0;


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
                    discussCount += article.MessageCount!.All;
                }
                currentArticle = article;
            }

            if (currentArticle!.SearchDate <= currentRangeEndDate)
            {
                while (currentRangeEndDate < endDate)
                {

                    dateOfAnalysis.Add(currentRangeEndDate);
                    discussNumber.Add(discussCount);

                    if (stopFlag)
                        break;

                    discussCount = 0;


                    startDate = currentRangeEndDate;
                    currentRangeEndDate = startDate.AddDays(rangeWidth);

                    if (currentRangeEndDate >= endDate)
                    {
                        stopFlag = true;
                        currentRangeEndDate = endDate;
                    }
                }
            }

            var PopularityAnalysisResponse = new PopularityAnalysisResponse(DiscussNumber: discussNumber,
                                                                            Dates: dateOfAnalysis);
            return PopularityAnalysisResponse;
        }


    }
}

