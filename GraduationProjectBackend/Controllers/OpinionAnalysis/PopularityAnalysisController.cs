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
        /// <param name="Topic"> 搜尋主題 </param>
        /// <remarks>
        /// 給搜尋主題,搜索開始時間,搜索結束時間 \n
        /// 回傳
        /// </remarks>
        /// <returns></returns>

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PopularityAnalysis(PopularityAnalysisRequest re)
        {
            return Ok(await _popularityAnalysisService.GetPopularityAnalysisResponse(re.Topic, re.StartDate, re.EndDate));
        }
    }
}
