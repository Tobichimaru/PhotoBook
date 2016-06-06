using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Tag
    {
        public Tag()
        {
            Photos = new HashSet<Photo>();
        }

        [Key]
        public int TagId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}