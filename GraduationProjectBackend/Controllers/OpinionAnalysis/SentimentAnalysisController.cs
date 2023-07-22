using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.Services.SentimentAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBackend.Controllers.OpinionAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentimentAnalysisController : ControllerBase
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;

        public SentimentAnalysisController(ISentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }
        /// <summary>
        /// 正負向分析
        /// </summary>
        /// <remarks>
        /// </remarks>

        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        public async Task<ActionResult> SentimentAnalysis([FromRoute] SentimentAnalysisRequest popularityAnalysisRequest)
        {
            return Ok(await _sentimentAnalysisService.GetSentimentAnalysisResponse(popularityAnalysisRequest.Topic,
                                                                                   popularityAnalysisRequest.StartDate,
                                                                                   popularityAnalysisRequest.EndDate));
        }

        /// <summary>
        /// 假資料正負向分析
        /// </summary>
        /// <param name="popularityAnalysisRequest"></param>
        /// <param name="fakeSentimentAnalysisService"></param>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]

        public async Task<ActionResult> FakeSentimentAnalysis([FromRoute] SentimentAnalysisRequest popularityAnalysisRequest, [FromServices] FakeSentimentAnalysisService fakeSentimentAnalysisService)
        {
            return Ok(await fakeSentimentAnalysisService.GetSentimentAnalysisResponse(popularityAnalysisRequest.Topic,
                                                                                      popularityAnalysisRequest.StartDate,
                                                                                      popularityAnalysisRequest.EndDate));
        }
    }
}
