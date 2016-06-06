using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int PhotoId { get; set; }

        [Required]
        public virtual Photo LikedPhoto { get; set; }

    }
}