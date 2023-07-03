using GraduationProjectBackend.DataAccess.Models.Favorite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBackend.DataAccess.Models.Member
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [Required]
        public string? account { get; set; }
        [Required]
        public byte[]? password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public virtual ICollection<FavoriteFolder> FavoriteFolders { get; set; } = new List<FavoriteFolder>();



    }
}
