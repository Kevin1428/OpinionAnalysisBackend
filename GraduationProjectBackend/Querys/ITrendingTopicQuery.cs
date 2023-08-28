using GraduationProjectBackend.Querys.UserViews;
using GraduationProjectBackend.Services.OpinionAnalysis;

namespace GraduationProjectBackend.Querys
{
    public interface ITrendingTopicQuery
    {
        public Task<TrendingTopicUserView> GetTrendingTopicAsync(OpinionAnalysisParam param);
    }
}
