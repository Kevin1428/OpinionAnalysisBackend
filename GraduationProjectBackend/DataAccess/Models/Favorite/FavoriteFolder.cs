using GraduationProjectBackend.DataAccess.Models.Member;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GraduationProjectBackend.DataAccess.Models.Favorite
{
    [Table("FavoriteFolder")]
    public class FavoriteFolder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoriteFolderId { get; set; }
        public string? FavoriteFolderName { get; set; }
        public int UserId;
        [ForeignKey("UserId")]
        [JsonIgnore]
        public User? User { get; set; }
        public ICollection<FavoriteFolderItem> FavoriteFolderItems { get; set; } = new List<FavoriteFolderItem>();




    }
}
