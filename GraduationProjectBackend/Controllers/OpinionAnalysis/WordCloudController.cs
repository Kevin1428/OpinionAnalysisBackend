using GraduationProjectBackend.DataAccess.DTOs.OpinionAnalysis;
using GraduationProjectBackend.Services.OpinionAnalysis.WordCloud;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectBackend.Controllers.OpinionAnalysis
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordCloudController : ControllerBase
    {
        private readonly IWordCloudService _wordCloudService;

        public WordCloudController(IWordCloudService wordCloudService)
        {
            _wordCloudService = wordCloudService;
        }
        /// <summary>
        /// 正負向都有的文字雲
        /// </summary>
        /// <param name="Topic"> 搜尋主題 </param>
        /// <remarks>
        /// 搜尋主題內容斷詞後的詞和頻率
        /// </remarks>
        /// <returns></returns>

        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        //[ProducesResponseType(typeof(WordCloudResponseDTO), StatusCodes.Status200OK)]

        public async Task<ActionResult> TopicContentSegment([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query)
        {
            try
            {
                var wordCloudResponseDTO = await _wordCloudService.GetFullWordCloudResponseDTO(
                    route.Topic,
                    route.StartDate,
                    route.EndDate,
                    query.DateRange,
                    query.IsExactMatch
                    );

                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 正向
        /// </summary>
        /// <param name="wordCloudRequest"></param>
        /// <returns></returns>
        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}/Positive")]
        //[Authorize]
        //[ProducesResponseType(typeof(WordCloudResponseDTO), StatusCodes.Status200OK)]

        public async Task<ActionResult> TopicContentSegmentByPositive([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query)
        {
            try
            {
                var wordCloudResponseDTO = await _wordCloudService.GetPositiveWordCloudResponseDTO(
                route.Topic,
                route.StartDate,
                route.EndDate, query.DateRange, query.IsExactMatch);

                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// 負向
        /// </summary>
        /// <param name="wordCloudRequest"></param>
        /// <returns></returns>
        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}/Negative")]
        //[Authorize]
        //[ProducesResponseType(typeof(WordCloudResponseDTO), StatusCodes.Status200OK)]

        public async Task<ActionResult> TopicContentSegmentByNegative([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query)
        {
            try
            {
                var wordCloudResponseDTO = await _wordCloudService.GetNegativeWordCloudResponseDTO(route.Topic, route.StartDate, route.EndDate, query.DateRange, query.IsExactMatch);


                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 假資料文字雲
        /// </summary>
        /// <returns></returns>
        [HttpGet("fake/{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        public async Task<ActionResult> FakeTopicContentSegment([FromRoute] OpinionAnalysisRequest.Route route, [FromQuery] OpinionAnalysisRequest.Query query, [FromServices] FakeWordCloudService fakeWordCloudService)
        {
            try
            {
                var wordCloudResponseDTO = await fakeWordCloudService.GetFullWordCloudResponseDTO(route.Topic, route.StartDate, route.EndDate, query.DateRange, query.IsExactMatch);


                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
