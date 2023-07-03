using GraduationProjectBackend.DataAccess.Models.Favorite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Favorite
{
    public class FavoriteItemSeedConfiguration : IEntityTypeConfiguration<FavoriteItem>
    {
        public void Configure(EntityTypeBuilder<FavoriteItem> builder)
        {
            builder.HasData(
                new FavoriteItem { FavoriteItemId = 1, FavoriteItemName = "館長" },
                new FavoriteItem { FavoriteItemId = 2, FavoriteItemName = "廖老大" },
                new FavoriteItem { FavoriteItemId = 3, FavoriteItemName = "總統" },
                new FavoriteItem { FavoriteItemId = 4, FavoriteItemName = "蔡英文" },
                new FavoriteItem { FavoriteItemId = 5, FavoriteItemName = "選舉" }
            );
        }
    }
}
