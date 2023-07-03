using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Member
{
    public class UserRoleSeedConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId);

            builder.HasData(
            new UserRole
            {
                UserRoleId = 1,
                UserId = 10000,
                RoleId = 1,
            },
            new UserRole
            {
                UserRoleId = 2,
                UserId = 10001,
                RoleId = 1,
            },
            new UserRole
            {
                UserRoleId = 3,
                UserId = 10002,
                RoleId = 1,
            }

            );
        }
    }
}
