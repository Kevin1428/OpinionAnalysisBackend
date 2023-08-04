namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs
{
    public class WordCloudResponse
    {

        public ICollection<string> WordSegment { get; set; }
        public ICollection<int> Frequency { get; set; }
        public WordCloudResponse(ICollection<string> wordSegment, ICollection<int> frequency)
        {
            WordSegment = wordSegment;
            Frequency = frequency;
        }

    }
}
