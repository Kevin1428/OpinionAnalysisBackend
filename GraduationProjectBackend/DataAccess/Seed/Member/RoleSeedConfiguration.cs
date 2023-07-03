using GraduationProjectBackend.DataAccess.Models.Member;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduationProjectBackend.DataAccess.Seed.Member
{
    public class RoleSeedConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role { roleId = 1, roleName = "Users" },
                new Role { roleId = 2, roleName = "Admin" }
                );
        }
    }


}
