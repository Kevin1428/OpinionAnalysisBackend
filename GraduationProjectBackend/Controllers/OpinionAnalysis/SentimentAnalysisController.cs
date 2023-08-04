﻿using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis;
using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis.SentimentAnalysis;
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
                                                                                   query.dateRange,
                                                                                   query.isExactMatch
                                                                                   ));
        }

        /// <summary>
        /// 假資料正負向分析
        /// </summary>
        /// <param name="route"></param>
        /// <param name="fakeSentimentAnalysisService"></param>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]

        public async Task<ActionResult> FakeSentimentAnalysis([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query, [FromServices] FakeSentimentAnalysisService fakeSentimentAnalysisService)
        {
            return Ok(await fakeSentimentAnalysisService.GetSentimentAnalysisResponse(route.Topic,
                                                                                      route.StartDate,
                                                                                      route.EndDate,
                                                                                      query.dateRange, query.isExactMatch));
        }
    }
}
