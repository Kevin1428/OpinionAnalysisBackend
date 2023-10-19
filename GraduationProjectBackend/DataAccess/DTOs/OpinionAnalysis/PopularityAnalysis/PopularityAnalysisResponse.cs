using GraduationProjectBackend.Enums;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.PopularityAnalysis
{
    public record PopularityAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> DiscussNumber,
        IDictionary<AddressType, int> AddressDiscussNumber,
        IDictionary<DateOnly, ICollection<ArticleUserView>> HotArticles = null
        );


}
