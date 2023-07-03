using GraduationProjectBackend.DataAccess.Models.Favorite;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBackend.DataAccess.DTOs
{
    public class FavoriteFolderItemDTO
    {
        public int FavoriteFolderId { get; set; }
        public int FavoriteItemId { get; private set; }
        public FavoriteItem? FavoriteItem { get; set; }
    }
}
