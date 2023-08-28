using static GraduationProjectBackend.Querys.UserViews.TrendingTopicUserView;

namespace GraduationProjectBackend.Querys.UserViews
{
    public record TrendingTopicUserView(int ArticleNumber, float FindPercentage, IEnumerable<TrendingTopicDto> TrendingTopics)
    {
        public record TrendingTopicDto(string Topic, int Fequency, float DiscussPercentage);

    }
}
