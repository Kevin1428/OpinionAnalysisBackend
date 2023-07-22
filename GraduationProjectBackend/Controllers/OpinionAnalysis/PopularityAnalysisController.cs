using GraduationProjectBackend.DataAccess.DTOs.PopularityAnalysis;
using GraduationProjectBackend.Services.PopularityAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBackend.Controllers.OpinionAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class PopularityAnalysisController : ControllerBase
    {
        private readonly IPopularityAnalysisService _popularityAnalysisService;

        public PopularityAnalysisController(IPopularityAnalysisService popularityAnalysisService)
        {
            _popularityAnalysisService = popularityAnalysisService;
        }
        /// <summary>
        /// 熱度分析
        /// </summary>
        /// <remarks>
        /// </remarks>

        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        public async Task<ActionResult> PopularityAnalysis([FromRoute] PopularityAnalysisRequest popularityAnalysisRequest)
        {
            return Ok(await _popularityAnalysisService.GetPopularityAnalysisResponse(
                popularityAnalysisRequest.Topic,
                popularityAnalysisRequest.StartDate,
                popularityAnalysisRequest.EndDate));
        }

        /// <summary>
        /// 假資料熱度分析
        /// </summary>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        public async Task<ActionResult> FakePopularityAnalysis([FromRoute] PopularityAnalysisRequest popularityAnalysisRequest, [FromServices] FakePopularityAnalysisService fakePopularityAnalysisService)
        {
            return Ok(await fakePopularityAnalysisService.GetPopularityAnalysisResponse(
                popularityAnalysisRequest.Topic,
                popularityAnalysisRequest.StartDate,
                popularityAnalysisRequest.EndDate));
        }
    }
}
