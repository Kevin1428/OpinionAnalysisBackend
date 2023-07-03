using GraduationProjectBackend.DataAccess.Models.Favorite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Favorite
{
    public class FavoriteFolderItemSeedConfiguration : IEntityTypeConfiguration<FavoriteFolderItem>
    {
        public void Configure(EntityTypeBuilder<FavoriteFolderItem> builder)
        {
            builder.HasData(
                new FavoriteFolderItem { favoriteFolderItemId = 1, FavoriteFolderId = 1, FavoriteItemId = 1 },
                new FavoriteFolderItem { favoriteFolderItemId = 2, FavoriteFolderId = 1, FavoriteItemId = 2 },
                new FavoriteFolderItem { favoriteFolderItemId = 3, FavoriteFolderId = 1, FavoriteItemId = 3 },

                new FavoriteFolderItem { favoriteFolderItemId = 4, FavoriteFolderId = 2, FavoriteItemId = 2 },
                new FavoriteFolderItem { favoriteFolderItemId = 5, FavoriteFolderId = 2, FavoriteItemId = 3 },

                new FavoriteFolderItem { favoriteFolderItemId = 6, FavoriteFolderId = 3, FavoriteItemId = 4 },
                new FavoriteFolderItem { favoriteFolderItemId = 7, FavoriteFolderId = 3, FavoriteItemId = 5 },

                new FavoriteFolderItem { favoriteFolderItemId = 8, FavoriteFolderId = 4, FavoriteItemId = 1 },

                new FavoriteFolderItem { favoriteFolderItemId = 9, FavoriteFolderId = 5, FavoriteItemId = 4 },
                new FavoriteFolderItem { favoriteFolderItemId = 10, FavoriteFolderId = 5, FavoriteItemId = 5 }

                );
        }
    }
}
