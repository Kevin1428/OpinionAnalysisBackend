﻿using GraduationProjectBackend.DataAccess.DTOs.SentimentAnalysis;
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
        public async Task<ActionResult> PopularityAnalysis([FromRoute] SentimentAnalysisRequest popularityAnalysisRequest)
        {
            return Ok(await _popularityAnalysisService.GetPopularityAnalysisResponse(
                popularityAnalysisRequest.Topic,
                popularityAnalysisRequest.StartDate,
                popularityAnalysisRequest.EndDate));
        }

        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        public async Task<ActionResult> FakePopularityAnalysis([FromRoute] SentimentAnalysisRequest popularityAnalysisRequest, [FromServices] FakePopularityAnalysisService fakePopularityAnalysisService)
        {
            return Ok(await fakePopularityAnalysisService.GetPopularityAnalysisResponse(
                popularityAnalysisRequest.Topic,
                popularityAnalysisRequest.StartDate,
                popularityAnalysisRequest.EndDate));
        }
    }
}
