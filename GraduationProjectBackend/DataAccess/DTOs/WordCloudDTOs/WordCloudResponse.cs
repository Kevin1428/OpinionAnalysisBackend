namespace GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs
{
    public class WordCloudResponse
    {

        public ICollection<String> WordSegment { get; set; }
        public ICollection<int> Frequency { get; set; }
        public WordCloudResponse(ICollection<string> wordSegment, ICollection<int> frequency)
        {
            WordSegment = wordSegment;
            Frequency = frequency;
        }

    }
}
