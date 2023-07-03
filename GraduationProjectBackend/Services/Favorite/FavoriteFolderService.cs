using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Repositories.Favorite;

namespace GraduationProjectBackend.Services.Favorite
{
    public class FavoriteFolderService : IFavoriteFolderService
    {
        private readonly FavoriteFolderRepository _favoriteFolderRepository;
        private readonly FavoriteFolderItemRepository _favoriteFolderItemRepository;
        private readonly FavoriteItemRepository _favoriteItemRepository;

        public FavoriteFolderService(FavoriteFolderRepository favoriteFolderRepository, FavoriteFolderItemRepository favoriteFolderItemRepository, FavoriteItemRepository favoriteItemRepository)

        {
            _favoriteFolderRepository = favoriteFolderRepository;
            _favoriteFolderItemRepository = favoriteFolderItemRepository;
            _favoriteItemRepository = favoriteItemRepository;

        }



        public async Task<List<FavoriteFolder>> GetAllFavoriteFolders(int userId)
        {
            return await _favoriteFolderRepository.GetAllByCondition(ff => ff.UserId == userId);
        }
        public async Task<FavoriteFolder?> AddFavoriteFolder(int userId, string favoriteFolderName)
        {
            FavoriteFolder? favoriteFolder = new FavoriteFolder();
            favoriteFolder.UserId = userId;
            favoriteFolder.FavoriteFolderName = favoriteFolderName;

            await _favoriteFolderRepository.Add(favoriteFolder);
            favoriteFolder = await _favoriteFolderRepository.GetByCondition(ff => ff.FavoriteFolderName == favoriteFolderName);

            return favoriteFolder;
        }
        public async Task UpdateFavoriteFolder(int FavoriteFolderId, string favoriteFolderItemName)
        {
            FavoriteFolder? favoriteFolder = await _favoriteFolderRepository.GetByID(FavoriteFolderId);
            if (favoriteFolder != null)
            {
                favoriteFolder.FavoriteFolderName = favoriteFolderItemName;
                await _favoriteFolderRepository.Update(favoriteFolder);
            }
        }
        public async Task DeleteFavoriteFolder(int FavoriteFolderId)
        {
            await _favoriteFolderRepository.DeleteById(FavoriteFolderId);
        }
        public async Task DeleteFavoriteFolder(FavoriteFolder favoriteFolder)
        {
            await _favoriteFolderRepository.DeleteByEntity(favoriteFolder);
        }



        public async Task<FavoriteFolderItem?> GetFavoriteFolderItem(int FavoriteFolderItemId)
        {
            return await _favoriteFolderItemRepository.GetByID(FavoriteFolderItemId);
        }
        public async Task<List<FavoriteFolderItem>> GetAllFavoriteFolderItem(int FavoriteFolderId)
        {
            return await _favoriteFolderItemRepository.GetAllByCondition(fi => fi.FavoriteFolderId == FavoriteFolderId);
        }
        public async Task<FavoriteFolderItem?> AddFavoriteFolderItem(int FavoriteFolderId, string favoriteItemName)
        {
            FavoriteFolderItem favoriteFolderItem = new FavoriteFolderItem();
            favoriteFolderItem.FavoriteFolderId = FavoriteFolderId;

            FavoriteItem? favoriteItem = await _favoriteItemRepository.GetByCondition(fi => fi.FavoriteItemName == favoriteItemName);

            favoriteItem ??= new FavoriteItem { FavoriteItemName = favoriteItemName };
            favoriteFolderItem.FavoriteItem = favoriteItem;

            await _favoriteFolderItemRepository.Add(favoriteFolderItem);

            return await _favoriteFolderItemRepository.GetByCondition(fI => fI.FavoriteItem.FavoriteItemName == favoriteItemName);

        }
        public async Task DeleteFavoriteFolderItem(int FavoriteFolderItemId)
        {
            await _favoriteFolderItemRepository.DeleteById(FavoriteFolderItemId);
        }
        public async Task DeleteFavoriteFolderItem(FavoriteFolderItem favoriteFolderItem)
        {
            await _favoriteFolderItemRepository.DeleteByEntity(favoriteFolderItem);
        }


    }

}

