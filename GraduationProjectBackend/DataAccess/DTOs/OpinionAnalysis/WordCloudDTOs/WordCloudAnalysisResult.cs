using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;

namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs
{
    public class WordCloudAnalysisResult
    {

        public ICollection<string> WordSegment { get; set; }
        public ICollection<int> WordSegmentFrequency { get; set; }
        public ICollection<string> WordSegmentNb { get; set; }
        public ICollection<int> WordSegmentNbFrequency { get; set; }
        public ICollection<string> WordSegmentAdj { get; set; }
        public ICollection<int> WordSegmentAdjFrequency { get; set; }

        public IDictionary<string, IEnumerable<ArticleUserView>> RelatedArticle { get; set; }


        public WordCloudAnalysisResult(ICollection<string> wordSegment, ICollection<int> wordSegmentFrequency, ICollection<string> wordSegmentNb, ICollection<int> wordSegmentNbFrequency, ICollection<string> wordSegmentAdj, ICollection<int> wordSegmentAdjFrequency, IDictionary<string, IEnumerable<ArticleUserView>> relatedArticle)
        {
            WordSegment = wordSegment;
            WordSegmentFrequency = wordSegmentFrequency;

            WordSegmentNb = wordSegmentNb;
            WordSegmentNbFrequency = wordSegmentNbFrequency;

            WordSegmentAdj = wordSegmentAdj;
            WordSegmentAdjFrequency = wordSegmentAdjFrequency;
            RelatedArticle = relatedArticle;
        }

    }
}
