namespace GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis
{
    public class PopularityAnalysisRequest
    {
        public string Topic { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
