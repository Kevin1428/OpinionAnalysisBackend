using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Models.Member;
using GraduationProjectBackend.DataAccess.Seed.Favorite;
using GraduationProjectBackend.DataAccess.Seed.Member;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectBackend.DataAccess.Context
{
    public class MssqlDbContext : DbContext
    {
        public MssqlDbContext(DbContextOptions<MssqlDbContext> options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<FavoriteFolder> FavoriteFolders { get; set; }
        public DbSet<FavoriteFolderItem> favoriteFolderItems { get; set; }
        public DbSet<FavoriteItem> FavoriteItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserSeedConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleSeedConfiguration());
            modelBuilder.ApplyConfiguration(new RoleSeedConfiguration());

            modelBuilder.ApplyConfiguration(new FavoriteFolderSeedConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteFolderItemSeedConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteItemSeedConfiguration());

        }



    }
}
