using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Name { get; set; }

    }
}