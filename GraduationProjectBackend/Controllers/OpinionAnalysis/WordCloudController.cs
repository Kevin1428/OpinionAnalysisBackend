using GraduationProjectBackend.DataAccess.DTOs.WordCloudDTOs;
using GraduationProjectBackend.Services.WordCloud;
using Microsoft.AspNetCore.Authorization;
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
        /// 搜尋主題內容斷詞後的詞和頻率
        /// </summary>
        /// <param name="Topic"> 搜尋主題 </param>
        /// <remarks>
        /// 搜尋主題內容斷詞後的詞和頻率
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        //[ProducesResponseType(typeof(WordCloudResponseDTO), StatusCodes.Status200OK)]

        public async Task<ActionResult> TopicContentSegment([FromQuery] string Topic)
        {
            try
            {
                WordCloudResponseDTO wordCloudResponseDTO = await _wordCloudService.GetWordCloudResponseDTO(Topic);
                return Ok(wordCloudResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
