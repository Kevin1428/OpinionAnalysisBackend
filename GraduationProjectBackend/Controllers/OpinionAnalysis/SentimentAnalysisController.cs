using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis;
using GraduationProjectBackend.Services.OpinionAnalysis.SentimentAnalysis;
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
        public async Task<ActionResult> SentimentAnalysis([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query)
        {
            return Ok(await _sentimentAnalysisService.GetSentimentAnalysisResponse(route.Topic,
                                                                                   route.StartDate,
                                                                                   route.EndDate,
                                                                                   query.DateRange,
                                                                                   query.IsExactMatch, query.SearchMode));
        }
    }
}
