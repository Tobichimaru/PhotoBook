using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using Microsoft.Ajax.Utilities;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;
using MvcPL.Models.User;

namespace MvcPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _service;

        public HomeController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index(string filter = null)
        {
            ViewBag.filter = filter;
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                Content = new List<PhotoViewModel>(),
                PageSize = GalleryHelper.PageSize,
                CurrentPage = 1
            };

            _service.GetAllEntities().ForEach(u => u.Profile.Photos.ForEach(p => photos.Content.Add(p.ToMvcPhoto())));
            photos.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));
            photos.PageName = "Index";
            photos.Content = photos.Content.Where(x => filter == null || (x.Description.Contains(filter))).ToList();

            HttpContext.Session[User.Identity.Name + photos.PageName] = photos;

            return View(photos);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UsersEdit()
        {
            var model = _service.GetAllEntities().Select(u => new UserViewModel
            {
                Email = u.Email,
                Name = u.UserName
            });

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string name)
        {
            var user = _service.GetUserByName(name);
            _service.Delete(user);
            return RedirectToAction("UsersEdit");
        }


        [HttpPost]
        public ActionResult LinksView(int page, string pageName)
        {
            PagedList<PhotoViewModel> photos =
                (PagedList<PhotoViewModel>) HttpContext.Session[User.Identity.Name + pageName];
            photos.CurrentPage = page;

            List<PhotoViewModel> result =
                new List<PhotoViewModel>(
                    photos.Content.Skip(photos.PageSize*(photos.CurrentPage - 1)).Take(photos.PageSize));

            return PartialView("Links", new GalleryLinksModel
            {
                photos = result,
                page = page,
                count = photos.Content.Count,
                pageSize = photos.PageSize,
                pageName = pageName
            });
        }
    }
}