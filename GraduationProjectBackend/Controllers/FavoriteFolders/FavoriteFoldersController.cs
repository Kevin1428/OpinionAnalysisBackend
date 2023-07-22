using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.DTOs;
using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Repositories.Favorite;
using GraduationProjectBackend.Helper.Member;
using GraduationProjectBackend.Services.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBackend.Controllers.FavoriteFolders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class FavoriteFoldersController : ControllerBase
    {
        private readonly MssqlDbContext _context;
        private readonly FavoriteFolderRepository _favoriteFolderRepository;
        private readonly IFavoriteFolderService _favoriteFolderService;

        public FavoriteFoldersController(MssqlDbContext context, FavoriteFolderRepository favoriteFolderRepository, IFavoriteFolderService favoriteFolderService)
        {
            _context = context;
            _favoriteFolderRepository = favoriteFolderRepository;
            _favoriteFolderService = favoriteFolderService;
        }

        /// <summary>
        /// 取得該用戶所有資料夾
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteFolder>>> GetAllFavoriteFolders()
        {
            int userId = TokenParserHelper.GetUserId(HttpContext);
            IEnumerable<FavoriteFolder> favoriteFolders = await _favoriteFolderService.GetAllFavoriteFolders(userId);
            if (favoriteFolders == null)
            {
                return NotFound();
            }
            return Ok(favoriteFolders);
        }

        /// <summary>
        /// 取得資料夾下所有項目
        /// </summary>
        /// <param name="favoriteFolderId"></param>
        /// <returns></returns>
        [HttpGet("{favoriteFolderId}")]
        public async Task<ActionResult<List<FavoriteFolderItem>>> GetALLFavoriteFolderItem([FromRoute] int favoriteFolderId)
        {
            return await _favoriteFolderService.GetAllFavoriteFolderItem(favoriteFolderId);
        }

        /// <summary>
        /// 更改資料夾名稱
        /// </summary>
        /// <param name="favoriteFolderId"></param>
        /// <param name="favoriteFolderName"></param>
        /// <returns></returns>
        [HttpPatch("{favoriteFolderId}")]
        public async Task<IActionResult> PatchFavoriteFolder([FromRoute] int favoriteFolderId, string favoriteFolderName)
        {
            int userId = TokenParserHelper.GetUserId(HttpContext);

            try
            {
                await _favoriteFolderService.UpdateFavoriteFolder(favoriteFolderId, favoriteFolderName);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }
        /// <summary>
        /// 新增資料夾到該用戶底下
        /// </summary>
        /// <param name="favoriteFolderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FavoriteFolder>> PostFavoriteFolders(FavoriteFolderDTO favoriteFolderDTO)
        {
            int userId = TokenParserHelper.GetUserId(HttpContext);
            if (_context.FavoriteFolders == null)
            {
                return Problem("Entity set 'MssqlDbContext.FavoriteFolders'  is null.");
            }
            FavoriteFolder? favoriteFolder = await _favoriteFolderService.AddFavoriteFolder(userId, favoriteFolderDTO.FavoriteFolderName);

            return CreatedAtAction("GetALLFavoriteFolderItem", new { favoriteFolderId = favoriteFolder.FavoriteFolderId }, favoriteFolder);
        }

        /// <summary>
        /// 新增項目到資料夾底下
        /// </summary>
        /// <param name="favoriteFolderId"></param>
        /// <param name="favoriteFolderItemDTO"></param>
        /// <returns></returns>
        [HttpPost("/FavoriteFolder/{favoriteFolderId}/FavoriteFolderItem")]
        public async Task<ActionResult> PostFavoriteFolderItem([FromRoute] int favoriteFolderId, [FromBody] FavoriteFolderItemDTO favoriteFolderItemDTO)
        {
            await _favoriteFolderService.AddFavoriteFolderItem(favoriteFolderId, favoriteFolderItemDTO.FavoriteItemName);

            return Ok();
        }

        /// <summary>
        /// 刪除資料夾
        /// </summary>
        /// <param name="favoriteFolderId"></param>
        /// <returns></returns>
        [HttpDelete("{favoriteFolderId}")]
        public async Task<IActionResult> DeleteFavoriteFolder([FromRoute] int favoriteFolderId)
        {
            FavoriteFolder? favoriteFolder = await _favoriteFolderRepository.GetByID(favoriteFolderId);

            if (favoriteFolder == null)
            {
                return NotFound();
            }

            await _favoriteFolderService.DeleteFavoriteFolder(favoriteFolder);

            return NoContent();
        }

        /// <summary>
        /// 刪除資料夾內的項目
        /// </summary>
        /// <param name="favoriteFolderItemId"></param>
        /// <returns></returns>
        [HttpDelete("FavoriteFolderItem/{favoriteFolderItemId}")]
        public async Task<IActionResult> DeletefavoriteFolderItem([FromRoute] int favoriteFolderItemId)
        {
            var favoriteFolderItem = await _favoriteFolderService.GetFavoriteFolderItem(favoriteFolderItemId);

            if (favoriteFolderItem == null)
            {
                return NotFound();
            }

            await _favoriteFolderService.DeleteFavoriteFolderItem(favoriteFolderItem);

            return NoContent();
        }



    }
}
