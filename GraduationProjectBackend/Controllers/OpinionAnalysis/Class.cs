using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis;
using GraduationProjectBackend.Helper.Redis;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBackend.Controllers.OpinionAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        public async Task<ActionResult> PopularityAnalysis([FromRoute] OpinionAnalysisRequest.Route route,
            [FromQuery] OpinionAnalysisRequest.Query query)
        {
            return Ok(RedisHelper.GetRedisKey(route.Topic, route.StartDate, route.EndDate, query.DateRange, query.IsExactMatch, query.SearchMode));
        }
    }
}
