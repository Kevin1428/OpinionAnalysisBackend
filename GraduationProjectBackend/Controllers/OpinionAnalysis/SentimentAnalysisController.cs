using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
using GraduationProjectBackend.Services.SentimentAnalysis;
using Microsoft.AspNetCore.Authorization;
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
        /// 熱度分析
        /// </summary>
        /// <remarks>
        /// </remarks>

        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        [Authorize]
        public async Task<ActionResult> SentimentAnalysis([FromRoute] SentimentAnalysisRequest popularityAnalysisRequest)
        {
            return Ok(await _sentimentAnalysisService.GetSentimentAnalysisResponse(popularityAnalysisRequest.Topic, popularityAnalysisRequest.StartDate, popularityAnalysisRequest.EndDate));
        }
    }
}
