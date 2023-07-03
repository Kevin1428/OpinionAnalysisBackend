using GraduationProjectBackend.DataAccess.Models.Favorite;
using GraduationProjectBackend.DataAccess.Models.Member;
using GraduationProjectBackend.Helper.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Member
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    userId = 10000,
                    account = "user1",
                    password = EncryptHelper.EncryptStatic("user1"),
                },
                 new User
                 {
                     userId = 10001,
                     account = "user2",
                     password = EncryptHelper.EncryptStatic("user2"),
                 },
                 new User
                 {
                     userId = 10002,
                     account = "user3",
                     password = EncryptHelper.EncryptStatic("user3"),
                 }
            );
        }
    }
}
