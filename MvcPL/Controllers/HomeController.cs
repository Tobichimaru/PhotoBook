using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
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
        private readonly IUserService _Service;

        public HomeController(IUserService Service)
        {
            _Service = Service;
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

            _Service.GetAllEntities().ForEach(u => u.Profile.Photos.ForEach(p => photos.Content.Add(p.ToMvcPhoto(u.Profile.UserName))));
            photos.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));
            photos.PageName = "Index";
            photos.Content = photos.Content.Where(x => filter == null || (x.Description.Contains(filter))).ToList();

            HttpContext.Session[User.Identity.Name + photos.PageName] = photos;

            return View(photos);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult UsersEdit()
        {
            var model = _Service.GetAllEntities().Select(u => new UserViewModel
            {
                Email = u.Email,
                Name = u.UserName
            });

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string name)
        {
            var user = _Service.GetUserByName(name);
            _Service.Delete(user);
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


        [Route("tag/{name}")]
        public ActionResult TagSearch(string name)
        {
            var model = _Service.GetAllEntities();
            PagedList<PhotoViewModel> photos = new PagedList<PhotoViewModel>
            {
                Content = new List<PhotoViewModel>(),
                PageSize = GalleryHelper.PageSize,
                CurrentPage = 1
            };

            foreach (var user in model)
            {
                foreach (var photo in user.Profile.Photos)
                {
                    var flag = false;
                    foreach (var tag in photo.Tags)
                    {
                        if (tag.Name == name)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) photos.Content.Add(photo.ToMvcPhoto(user.Profile.UserName));
                }
            }

            photos.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));
            photos.PageName = "Tag" + name;

            HttpContext.Session[User.Identity.Name + photos.PageName] = photos;

            return View(photos);
        }
    }
}