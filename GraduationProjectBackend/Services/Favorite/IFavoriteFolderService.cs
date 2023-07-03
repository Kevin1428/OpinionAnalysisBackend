using GraduationProjectBackend.DataAccess.Models.Favorite;

namespace GraduationProjectBackend.Services.Favorite
{
    public interface IFavoriteFolderService
    {
        Task<List<FavoriteFolder>> GetAllFavoriteFolders(int userId);
        Task<FavoriteFolder?> AddFavoriteFolder(int userId, String favoriteFolderName);
        Task UpdateFavoriteFolder(int FavoriteFolderId, String favoriteFolderItemName);
        Task DeleteFavoriteFolder(int FavoriteFolderId);
        Task DeleteFavoriteFolder(FavoriteFolder favoriteFolder);


        Task<List<FavoriteFolderItem>> GetAllFavoriteFolderItem(int userId);
        Task<FavoriteFolderItem?> GetFavoriteFolderItem(int FavoriteFolderId);
        Task<FavoriteFolderItem?> AddFavoriteFolderItem(int FavoriteFolderId, String favoriteFolderItemName);
        Task DeleteFavoriteFolderItem(int FavoriteFolderItemId);
        Task DeleteFavoriteFolderItem(FavoriteFolderItem favoriteFolderItem);






    }
}
