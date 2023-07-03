using GraduationProjectBackend.DataAccess.Models.Favorite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Favorite
{
    public class FavoriteFolderSeedConfiguration : IEntityTypeConfiguration<FavoriteFolder>
    {
        public void Configure(EntityTypeBuilder<FavoriteFolder> builder)
        {


            builder.HasData(
                new FavoriteFolder
                {
                    UserId = 10000,
                    FavoriteFolderId = 1,
                    FavoriteFolderName = "資料夾一"
                },
                new FavoriteFolder
                {
                    UserId = 10000,
                    FavoriteFolderId = 2,
                    FavoriteFolderName = "資料夾二"
                },
                new FavoriteFolder
                {
                    UserId = 10000,
                    FavoriteFolderId = 3,
                    FavoriteFolderName = "資料夾三"
                },
                new FavoriteFolder
                {
                    UserId = 10001,
                    FavoriteFolderId = 4,
                    FavoriteFolderName = "資料夾一"
                },
                new FavoriteFolder
                {
                    UserId = 10001,
                    FavoriteFolderId = 5,
                    FavoriteFolderName = "資料夾二"
                }
                );
        }
    }
}
