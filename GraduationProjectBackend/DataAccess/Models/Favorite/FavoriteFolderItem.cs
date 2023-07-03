using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GraduationProjectBackend.DataAccess.Models.Favorite
{
    [Table("FavoriteFolderItem")]
    public class FavoriteFolderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int favoriteFolderItemId { get; set; }

        [JsonIgnore]
        public int FavoriteFolderId { get; set; }

        [ForeignKey("FavoriteFolderId")]
        [Required]
        [JsonIgnore]
        public FavoriteFolder? FavoriteFolder { get; set; }

        public int FavoriteItemId { get; set; }

        [ForeignKey("FavoriteItemId")]

        public FavoriteItem? FavoriteItem { get; set; }

    }
}
