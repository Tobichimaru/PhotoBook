using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Interfacies.DTO;

namespace MvcPL.Models.Photo
{
    public class PhotoViewModel
    {
        public PhotoViewModel()
        {
            CreatedOn = DateTime.Now;
        }

        public String Description { get; set; }
        public String ImagePath { get; set; }
        public String ThumbPath { get; set; }
        public DateTime CreatedOn { get; set; }

        public byte[] Picture { get; set; }
        public byte[] FullSize { get; set; }
        public string UserName { get; set; }

        public virtual List<TagModel> Tags { get; set; }

    }
}