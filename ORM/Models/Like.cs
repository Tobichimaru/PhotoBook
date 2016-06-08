using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

    }
}