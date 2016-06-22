using MvcPL.Models.Photo;

namespace MvcPL.Models
{
    public class UserPageModel
    {
        public ProfileViewModel profile { get; set; }
        public PagedList<PhotoViewModel> photos { get; set; }
    }
}