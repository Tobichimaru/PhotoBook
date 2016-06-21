﻿using System.Collections.Generic;
using MvcPL.Models.Photo;

namespace MvcPL.Models
{
    public class GalleryLinksModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<PhotoViewModel> photos { get; set; }
    }
}