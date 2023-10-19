using GraduationProjectBackend.ConfigModel;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.WordCloudDTOs;
using GraduationProjectBackend.Enums;
using GraduationProjectBackend.Utility.ArticleReader;
using GraduationProjectBackend.Utility.ArticleReader.ArticleModel;
using Microsoft.Extensions.Options;

namespace GraduationProjectBackend.Services.OpinionAnalysis.WordCloud
{
    public class WordCloudService : IWordCloudService
    {

        private readonly ArticleHelper _articleHelper;
        private readonly OpinionAnalysisConfig _opinionAnalysisConfig;


        public WordCloudService(ArticleHelper articleHelper, IOptions<OpinionAnalysisConfig> opinionAnalysisConfig)
        {
            _articleHelper = articleHelper;
            _opinionAnalysisConfig = opinionAnalysisConfig.Value;
        }

        public async Task<WordCloudAnalysisResult> GetFullWordCloudResponseDTO(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode,
            IEnumerable<AddressType>? addressTypes)
        {
            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch, addressTypes);

            bool ContentFilter(Article a) => true;
            bool ArticleTitleFilter(Article a) => true;
            bool MessagesContentFilter(Message a) => true;

            return await GetWordCloudResponse(article, ContentFilter, ArticleTitleFilter, MessagesContentFilter);
        }
        public async Task<WordCloudAnalysisResult> GetPositiveWordCloudResponseDTO(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode,
            IEnumerable<AddressType>? addressTypes)
        {
            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch, addressTypes);
            bool ContentFilter(Article a) => a.ArticleTitleSentiment.Contains("positive");
            bool ArticleTitleFilter(Article a) => a.ContentSentiment.Contains("positive");
            bool MessagesContentFilter(Message a) => a.PushContentSentiment!.Contains("positive");

            return await GetWordCloudResponse(article, ContentFilter, ArticleTitleFilter, MessagesContentFilter);

        }
        public async Task<WordCloudAnalysisResult> GetNegativeWordCloudResponseDTO(string topic, DateOnly startDate,
            DateOnly endDate, int dateRange, bool? isExactMatch, SearchModeEnum searchMode,
            IEnumerable<AddressType>? addressTypes)
        {
            var article = await _articleHelper.GetArticlesInDateRange(topic, startDate, endDate, dateRange, isExactMatch, addressTypes);

            bool ContentFilter(Article a) => a.ArticleTitleSentiment.Contains("negative");
            bool ArticleTitleFilter(Article a) => a.ContentSentiment.Contains("negative");
            bool MessagesContentFilter(Message a) => a.PushContentSentiment!.Contains("negative");

            return await GetWordCloudResponse(article, ContentFilter, ArticleTitleFilter, MessagesContentFilter);

        }
        public async Task<WordCloudAnalysisResult> GetWordCloudResponse(List<Article> article,
    Func<Article, bool> contentFilter, Func<Article, bool> articleTitleFilter, Func<Message, bool> messagesContentFilter)
        {
            var nbWords = new List<string>();
            var adjWords = new List<string>();
            var words = new List<string>();
            var takeHowMany = _opinionAnalysisConfig.WordCloudTakeNumber;

            words.AddRange(article.Where(contentFilter).SelectMany(A => A.ProcessedContent));
            words.AddRange(article.Where(articleTitleFilter).SelectMany(A => A.ProcessedArticleTitle!));
            words.AddRange(article.SelectMany(A =>
                A.Messages.Where(messagesContentFilter).SelectMany(M => M.ProcessedPushContent!)));

            var dic = words.Where(o => o.Length > _opinionAnalysisConfig.WordSegmentWordLength).GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(D => D.Value).Take(takeHowMany);
            var wordSegment = dic.Select(pair => pair.Key).ToList();
            var frequency = dic.Select(pair => pair.Value).ToList();

            nbWords.AddRange(article.Where(contentFilter).SelectMany(A => A.ProcessedNbContent));
            nbWords.AddRange(article.Where(articleTitleFilter).SelectMany(A => A.ProcessedNbArticleTitle));
            nbWords.AddRange(article.SelectMany(A =>
                A.Messages.Where(messagesContentFilter).SelectMany(M => M.ProcessedNbPushContent!)));

            var nbDic = nbWords.Where(o => o.Length > _opinionAnalysisConfig.WordNbSegmentWordLength).GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(D => D.Value).Take(takeHowMany);
            var nbWordSegment = nbDic.Select(pair => pair.Key).ToList();
            var nbFrequency = nbDic.Select(pair => pair.Value).ToList();

            adjWords.AddRange(article.Where(contentFilter).SelectMany(A => A.ProcessedAdjContent));
            adjWords.AddRange(article.Where(articleTitleFilter).SelectMany(A => A.ProcessedAdjArticleTitle));
            adjWords.AddRange(article.SelectMany(A =>
                A.Messages.Where(messagesContentFilter).SelectMany(M => M.ProcessedAdjPushContent!)));

            var adjDic = adjWords.Where(o => o.Length > _opinionAnalysisConfig.WordAdjSegmentWordLength).GroupBy(word => word).ToDictionary(group => group.Key, group => group.Count())
                .OrderByDescending(D => D.Value).Take(takeHowMany);
            var adjWordSegment = adjDic.Select(pair => pair.Key).ToList();
            var adjFrequency = adjDic.Select(pair => pair.Value).ToList();

            var allWords = new List<string>();
            allWords.AddRange(dic.Select(o => o.Key));
            allWords.AddRange(adjDic.Select(o => o.Key));
            allWords.AddRange(nbDic.Select(o => o.Key));

            allWords = allWords.Distinct().ToList();

            var relateArticles = new Dictionary<string, IEnumerable<ArticleUserView>>();

            foreach (var word in allWords)
            {
                var wordArticles = article.Where(o => o.Content.Contains(word) || o.ArticleTitle.Contains(word) || o.Messages.Select(m => m.PushContent).Any(c => c.Contains(word)));

                foreach (var a in wordArticles)
                {
                    a.Messages = a.Messages.Where(o => o.PushContent!.Contains(word)).ToList();
                }
                relateArticles.Add(word, wordArticles.Select(o => o.ToAtricleUserView()));
            }

            return await Task.FromResult(new WordCloudAnalysisResult(wordSegment, frequency, nbWordSegment, nbFrequency,
                adjWordSegment, adjFrequency, relateArticles));
        }


    }

}
