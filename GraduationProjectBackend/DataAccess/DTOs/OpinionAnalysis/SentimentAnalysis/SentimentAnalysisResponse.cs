using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis
{
    public record SentimentAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> PositiveNumber,
        ICollection<int> NegativeNumber,
        IDictionary<DateOnly, ICollection<ArticleUserView>> PosHotArticle = null,
        IDictionary<DateOnly, ICollection<ArticleUserView>> NegHotArticle = null
     );
}
