using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBackend.DataAccess.Models.Favorite
{
    [Table("FavoriteItem")]
    public class FavoriteItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FavoriteItemId { get; set; }

        public string? FavoriteItemName { get; set; }

    }
}
