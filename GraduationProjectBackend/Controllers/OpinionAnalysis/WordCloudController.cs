using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using GraduationProjectBackend.Services.WordCloud;
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
        /// 文字雲
        /// </summary>
        /// <param name="Topic"> 搜尋主題 </param>
        /// <remarks>
        /// 搜尋主題內容斷詞後的詞和頻率
        /// </remarks>
        /// <returns></returns>

        [HttpGet("{Topic}/StatrDate/{StartDate}/EndDate/{EndDate}")]
        //[Authorize]
        //[ProducesResponseType(typeof(WordCloudResponseDTO), StatusCodes.Status200OK)]

        public async Task<ActionResult> TopicContentSegment([FromRoute] WordCloudRequest wordCloudRequest)
        {
            try
            {
                var wordCloudResponseDTO = await _wordCloudService.GetWordCloudResponseDTO(
                    wordCloudRequest.Topic,
                    wordCloudRequest.StartDate,
                    wordCloudRequest.EndDate);

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
        public async Task<ActionResult> FakeTopicContentSegment([FromRoute] WordCloudRequest wordCloudRequest, [FromServices] FakeWordCloudService fakeWordCloudService)
        {
            try
            {
                var wordCloudResponseDTO = await fakeWordCloudService.GetWordCloudResponseDTO(
                    wordCloudRequest.Topic,
                    wordCloudRequest.StartDate,
                    wordCloudRequest.EndDate);

                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
