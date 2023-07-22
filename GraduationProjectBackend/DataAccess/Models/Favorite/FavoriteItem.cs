using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GraduationProjectBackend.DataAccess.Models.Favorite
{
    [Table("FavoriteItem")]
    public class FavoriteItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int FavoriteItemId { get; set; }

        public string? FavoriteItemName { get; set; }

    }
}
