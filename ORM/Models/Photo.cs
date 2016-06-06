using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM.Models
{
    public class Photo
    {
        public Photo()
        {
            Tags = new HashSet<Tag>();
            Likes = new HashSet<Like>();
        }
        
        [Key]
        public int PhotoId { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User PublisherUser { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}