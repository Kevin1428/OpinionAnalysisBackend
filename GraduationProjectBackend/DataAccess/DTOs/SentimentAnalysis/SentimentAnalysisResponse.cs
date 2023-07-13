namespace GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis
{
    public record SentimentAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> PositiveNumber,
        ICollection<int> NegativeNumber
     );
}
