using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis
{
    public record PopularityAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> DiscussNumber,
        IDictionary<DateOnly, ICollection<ArticleUserView>> HotArticles = null
     );


}
