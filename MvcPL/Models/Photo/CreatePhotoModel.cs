﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models.Photo
{
    public class CreatePhotoModel
    {
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }

        [Display(Name = "Print tags separated by spaces")]
        [RegularExpression(@"[\w\s]+", ErrorMessage = "Tag should consists only of characters")]
        public string Tags { get; set; }
    }
}