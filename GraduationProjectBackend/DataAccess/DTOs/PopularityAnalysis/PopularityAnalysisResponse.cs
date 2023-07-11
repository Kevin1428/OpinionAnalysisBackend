namespace GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis
{
    public record PopularityAnalysisResponse(
        ICollection<DateOnly> Dates,
        ICollection<int> DiscussNumber
     );
}
