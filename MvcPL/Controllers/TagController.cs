using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;

namespace MvcPL.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _service;

        public TagController(ITagService service)
        {
            _service = service;
        }

        [Route("tag/{name}")]
        public ActionResult TagSearch(string name)
        {
            var tag = _service.GetTagByName(name);
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                Content = new List<PhotoViewModel>(tag.Photos.Select(p => p.ToMvcPhoto())),
                PageSize = GalleryHelper.PageSize,
                CurrentPage = 1
            };

            photos.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));
            photos.PageName = "Tag" + name;

            HttpContext.Session[User.Identity.Name + photos.PageName] = photos;

            return View(photos);
        }
	}
}