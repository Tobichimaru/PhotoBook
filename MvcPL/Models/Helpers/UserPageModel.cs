using MvcPL.Models.Photo;
using MvcPL.Models.Profile;

namespace MvcPL.Models.Helpers
{
    public class UserPageModel
    {
        public ProfileViewModel profile { get; set; }
        public PagedList<PhotoViewModel> photos { get; set; }
    }
}