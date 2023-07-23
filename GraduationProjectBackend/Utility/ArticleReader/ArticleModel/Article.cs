using System.Text.Json.Serialization;

namespace GraduationProjectBackend.Utility.ArticleReader.ArticleModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Article
    {
        //[JsonPropertyName("sentiment_count")]
        //public SentimentCount? sentiment_count { get; set; }

        public SentimentCount? sentiment_count { get; set; }


        [JsonPropertyName("article_id")]
        public string? ArticleId { get; set; }
        [JsonPropertyName("article_title")]
        public string? ArticleTitle { get; set; }
        [JsonPropertyName("author")]
        public string? Author { get; set; }
        [JsonPropertyName("board")]
        public string? Board { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }

        [JsonPropertyName("date")]
        public string? StringDate { get; set; }

        [JsonPropertyName("ip")]
        public string? Ip { get; set; }
        [JsonPropertyName("message_count")]
        public MessageCount? MessageCount { get; set; }
        [JsonPropertyName("messages")]
        public List<Message>? Messages { get; set; }
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("processed_content")]
        public IEnumerable<string> ProcessedContent { get; set; }
        [JsonPropertyName("processed_article_title")]
        public IEnumerable<string>? ProcessedArticleTitle { get; set; }
        public DateOnly SearchDate { get; set; }
        [JsonPropertyName("search_date")]
        public string searchDate
        {
            get => searchDate;
            set
            {
                SearchDate = DateOnly.Parse(value);
            }
        }
        [JsonPropertyName("content_sentiment")]
        public string ContentSentiment { get; set; }
    }


    public class Message
    {
        [JsonPropertyName("push_content")]
        public string? PushContent { get; set; }
        [JsonPropertyName("push_content_sentiment")]
        public string? PushContentSentiment { get; set; }

        [JsonPropertyName("push_ipdatetime")]
        public string? PushIpDateTime { get; set; }

        [JsonPropertyName("push_tag")]
        public string? PushTag { get; set; }

        [JsonPropertyName("push_userid")]
        public string? PushUserId { get; set; }
        [JsonPropertyName("processed_push_content")]
        public IEnumerable<string>? ProcessedPushContent { get; set; }
    }

    public class MessageCount
    {
        [JsonPropertyName("all")]
        public int All { get; set; }

        [JsonPropertyName("boo")]
        public int Boo { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("neutral")]
        public int Neutral { get; set; }

        [JsonPropertyName("push")]
        public int Push { get; set; }
    }

    public class SentimentCount
    {
        [JsonPropertyName("negative")]
        public int Negative { get; set; }
        [JsonPropertyName("positive")]
        public int Positive { get; set; }
    }

    public class ArticleMapRoot
    {
        [JsonPropertyName("articles")]
        public List<Article>? Articles { get; set; }
    }


}





















