﻿namespace GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs
{
    public class WordCloudResponse
    {

        public ICollection<string> WordSegment { get; set; }
        public ICollection<int> WordSegmentFrequency { get; set; }
        public ICollection<string> WordSegmentNb { get; set; }
        public ICollection<int> WordSegmentNbFrequency { get; set; }
        public ICollection<string> WordSegmentAdj { get; set; }
        public ICollection<int> WordSegmentAdjFrequency { get; set; }


        public WordCloudResponse(ICollection<string> wordSegment, ICollection<int> wordSegmentFrequency, ICollection<string> wordSegmentNb, ICollection<int> wordSegmentNbFrequency, ICollection<string> wordSegmentAdj, ICollection<int> wordSegmentAdjFrequency)
        {
            WordSegment = wordSegment;
            WordSegmentFrequency = wordSegmentFrequency;

            WordSegmentNb = wordSegmentNb;
            WordSegmentNbFrequency = wordSegmentNbFrequency;

            WordSegmentAdj = wordSegmentAdj;
            WordSegmentAdjFrequency = wordSegmentAdjFrequency;
        }

    }
}
