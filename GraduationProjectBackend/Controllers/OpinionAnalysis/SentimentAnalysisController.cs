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
        public async Task<ActionResult> SentimentAnalysis([FromRoute] SentimentAnalysisRequest.Route route, [FromQuery] SentimentAnalysisRequest.Query query)
        {
            return Ok(await _sentimentAnalysisService.GetSentimentAnalysisResponse(route.Topic,
                                                                                   route.StartDate,
                                                                                   route.EndDate,
                                                                                   query.dateRange));
        }

        /// <summary>
        /// 假資料正負向分析
        /// </summary>
        /// <param name="route"></param>
        /// <param name="fakeSentimentAnalysisService"></param>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]

        public async Task<ActionResult> FakeSentimentAnalysis([FromRoute] SentimentAnalysisRequest.Route route, [FromQuery] SentimentAnalysisRequest.Query query, [FromServices] FakeSentimentAnalysisService fakeSentimentAnalysisService)
        {
            return Ok(await fakeSentimentAnalysisService.GetSentimentAnalysisResponse(route.Topic,
                                                                                      route.StartDate,
                                                                                      route.EndDate,
                                                                                      query.dateRange));
        }
    }
}
