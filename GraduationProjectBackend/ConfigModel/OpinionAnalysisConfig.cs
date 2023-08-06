namespace GraduationProjectBackend.ConfigModel
{
    public class OpinionAnalysisConfig
    {
        public int DateRange { get; set; }
        public int WordCloudTakeNumber { get; set; }
        public double ElasticSearchScorePercentage { get; set; }
        public string? ElasticSearchArticleTiTleMinimumShouldMatch { get; set; }
        public string? ElasticSearchArticleContentMinimumShouldMatch { get; set; }
        public int ElasticScrollSize { get; set; }
        public int RedisExpireSecond { get; set; }
        public ICollection<string>? RemoveTagWord { get; set; }
    }
}
