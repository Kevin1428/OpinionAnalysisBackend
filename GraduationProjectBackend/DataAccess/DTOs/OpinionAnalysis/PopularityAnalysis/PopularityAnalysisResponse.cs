using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis
{
    public record PopularityAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> DiscussNumber,
        ICollection<WordCloudAnalysisResult> WordCloudAnalysisResults,
        IDictionary<DateOnly, ICollection<ArticleUserView>> HotArticles = null
     );


}
