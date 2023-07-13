using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.Services.PopularityAnalysis;
using GraduationProjectBackend.Services.WordCloud;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBackend.Controllers.OpinionAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularityAnalysisController : ControllerBase
    {
        private readonly IPopularityAnalysisService _popularityAnalysisService;

        public PopularityAnalysisController(IWordCloudService wordCloudService, IPopularityAnalysisService popularityAnalysisService)
        {
            _popularityAnalysisService = popularityAnalysisService;
        }
        /// <summary>
        /// 熱度分析
        /// </summary>
        /// <remarks>
        /// </remarks>

        [HttpGet("/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        [Authorize]
        public async Task<ActionResult> PopularityAnalysis([FromRoute] PopularityAnalysisRequest popularityAnalysisRequest)
        {
            return Ok(await _popularityAnalysisService.GetPopularityAnalysisResponse(popularityAnalysisRequest.Topic, popularityAnalysisRequest.StartDate, popularityAnalysisRequest.EndDate));
        }
    }
}
