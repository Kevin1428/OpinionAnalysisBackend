using GraduationProjectBackend.DataAccess.Context;
using GraduationProjectBackend.DataAccess.DTOs;
using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Repositories.Favorite;
using GraduationProjectBackend.Helper.Member;
using GraduationProjectBackend.Services.Favorite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBackend.Controllers
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

        // GET: api/FavoriteFolders
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

        // GET: api/FavoriteFolders/5
        [HttpGet("{favoriteFolderId}")]
        public async Task<ActionResult<List<FavoriteFolderItem>>> GetALLFavoriteFolderItem(int favoriteFolderId)
        {
            return await _favoriteFolderService.GetAllFavoriteFolderItem(favoriteFolderId);
        }

        // PUT: api/FavoriteFolders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{favoriteFolderId}")]
        public async Task<IActionResult> PatchFavoriteFolder(int favoriteFolderId, string favoriteFolderName)
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

        // POST: api/FavoriteFolders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/FavoriteFolders/5
        [HttpDelete("{favoriteFolderId}")]
        public async Task<IActionResult> DeleteFavoriteFolder(int favoriteFolderId)
        {
            FavoriteFolder? favoriteFolder = await _favoriteFolderRepository.GetByID(favoriteFolderId);

            if (favoriteFolder == null)
            {
                return NotFound();
            }

            await _favoriteFolderService.DeleteFavoriteFolder(favoriteFolder);

            return NoContent();
        }

        [HttpDelete("FavoriteFolderItem/{favoriteFolderItemId}")]
        public async Task<IActionResult> DeletefavoriteFolderItem(int favoriteFolderItemId)
        {
            FavoriteFolderItem? favoriteFolderItem = await _favoriteFolderService.GetFavoriteFolderItem(favoriteFolderItemId);

            if (favoriteFolderItem == null)
            {
                return NotFound();
            }

            await _favoriteFolderService.DeleteFavoriteFolderItem(favoriteFolderItem);

            return NoContent();
        }



    }
}
