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
        public async Task<ActionResult> PopularityAnalysis([FromRoute] PopularityAnalysisRequest.Route route, [FromQuery] PopularityAnalysisRequest.Query query)
        {
            return Ok(await _popularityAnalysisService.GetPopularityAnalysisResponse(
                route.Topic,
                route.StartDate,
                route.EndDate,
                query.dateRange));
        }

        /// <summary>
        /// 假資料熱度分析
        /// </summary>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        public async Task<ActionResult> FakePopularityAnalysis([FromRoute] PopularityAnalysisRequest.Route route, [FromQuery] PopularityAnalysisRequest.Query query, [FromServices] FakePopularityAnalysisService fakePopularityAnalysisService)
        {
            return Ok(await fakePopularityAnalysisService.GetPopularityAnalysisResponse(
                route.Topic,
                route.StartDate,
                route.EndDate,
                query.dateRange));
        }
    }
}
