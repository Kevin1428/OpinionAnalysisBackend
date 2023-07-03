using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBackend.DataAccess.Models.Member
{
    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }
        public int UserId { get; set; }

        [ForeignKey("FavoriteFolderId")]

        public User? User { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}
