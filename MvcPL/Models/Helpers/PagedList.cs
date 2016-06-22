using System;
using System.Collections.Generic;

namespace MvcPL.Models
{
    public class PagedList<T>
    {
        public List<T> Content { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }

        public string PageName { get; set; }
    }
}