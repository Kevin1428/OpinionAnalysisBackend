using GraduationProjectBackend.ConfigModel;
using Microsoft.Extensions.Options;
using Nest;

namespace GraduationProjectBackend.Querys
{
    public class QueryBase
    {
        protected ElasticClient client { get; set; }
        protected readonly OpinionAnalysisConfig _opinionAnalysisConfig;

        public QueryBase(IOptions<OpinionAnalysisConfig> opinionAnalysisConfig)
        {
            var node = new Uri("http://elasticsearch:9200");
            var settings = new ConnectionSettings(node).EnableApiVersioningHeader();
            client = new ElasticClient(settings);
            _opinionAnalysisConfig = opinionAnalysisConfig.Value;
        }
    }
}
