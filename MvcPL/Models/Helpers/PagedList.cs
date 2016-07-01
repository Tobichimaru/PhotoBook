using System.Collections.Generic;

namespace MvcPL.Models.Helpers
{
    public class PagedList<T>
    {
        public List<T> Content { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public string PageName { get; set; }
    }
}