using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Photo
{
    public class CreatePhotoModel
    {
        [Display(Name = "Description")]
        [Required]
        public String Description { get; set; }

        [Display(Name = "Image Path")]
        public String ImagePath { get; set; }

        [Display(Name = "Thumb Path")]
        public String ThumbPath { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        [Display(Name = "Print tags separated by spaces")]
        public string Tags { get; set; }
    }
}