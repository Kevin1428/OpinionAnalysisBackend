namespace GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs
{
    public class WordCloudResponse
    {

        public ICollection<String> WordSegment { get; set; }
        public ICollection<int> Frequency { get; set; }

    }
}
