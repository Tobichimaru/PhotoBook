using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}