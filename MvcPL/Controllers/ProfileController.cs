using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.Helpers;
using MvcPL.Models.Photo;
using MvcPL.Models.Profile;

namespace MvcPL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _Service;

        public ProfileController(IProfileService Service)
        {
            _Service = Service;
        }

        [Route("user/{name}")]
        public ActionResult UserPage(string name)
        {
            var profile = _Service.GetProfileByName(name);
            if (profile == null)
            {
                return View("Error");
            }

            var records = new PagedList<PhotoViewModel>();
            records.Content = new List<PhotoViewModel>();
            foreach (var item in _Service.GetProfileByName(name).Photos)
            {
                records.Content.Add(item.ToMvcPhoto(null));
            }
            records.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));

            // Count
            records.CurrentPage = 1;
            records.PageSize = GalleryHelper.PageSize;
            records.PageName = "Profile";

            HttpContext.Session[User.Identity.Name + records.PageName] = records;

            var model = new UserPageModel
            {
                photos = records,
                profile = profile.ToMvcProfile()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult ProfileEdit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ProfileEdit(ProfileEditModel viewModel, HttpPostedFileBase file)
        {
            var profile = _Service.GetProfileByName(User.Identity.Name);
            if (file != null && file.ContentLength > 0)
            {
                var target = new MemoryStream();
                file.InputStream.CopyTo(target);
                var byteArrayIn = target.ToArray();
                Image image = null;
                using (var ms = new MemoryStream(byteArrayIn))
                {
                    image = Image.FromStream(ms);
                }
                profile.Avatar = GalleryHelper.ImageToByteArray(GalleryHelper.CutImage(image, 150, 150));
            }

            profile.FirstName = viewModel.FirstName ?? profile.FirstName;
            profile.LastName = viewModel.LastName ?? profile.LastName;
            profile.Age = viewModel.Age == 0 ? profile.Age : viewModel.Age;

            _Service.Update(profile);
            return RedirectToAction("UserPage", new {name = profile.UserName});
        }

        [HttpGet]
        public ActionResult Create()
        {
            var photo = new CreatePhotoModel();
            return View(photo);
        }

        [HttpPost]
        public ActionResult Create(CreatePhotoModel photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(photo);

            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!httpPostedFileBases.Any() || httpPostedFileBases.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            photo.Tags = photo.Tags.Trim();

            var model = new PhotoViewModel();
            var profile = _Service.GetProfileByName(User.Identity.Name);

            foreach (var file in httpPostedFileBases)
            {
                if (file.ContentLength == 0) continue;

                using (var img = Image.FromStream(file.InputStream))
                {
                    var cutImage = GalleryHelper.CutImage(img, 300, 300);
                    model.Picture = GalleryHelper.ImageToByteArray(cutImage);

                    var newSize = new Size(600, 600);
                    var imgSize = GalleryHelper.NewImageSize(img.Size, newSize);
                    model.FullSize = GalleryHelper.ImageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));
                }
                // Save record to database
                model.CreatedOn = DateTime.Now;

                var tags = photo.Tags.Split(' ');
                model.Tags = new List<TagModel>();
                foreach (var tag in tags)
                {
                    model.Tags.Add(new TagModel
                    {
                        Name = tag
                    });
                }

                profile.Photos.Add(model.ToPhotoEntity());
            }
            _Service.Update(profile);
            return RedirectToAction("UserPage", new {name = profile.UserName});
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePhoto(string name, int photoId)
        {
            var profile = _Service.GetProfileByName(name);
            _Service.DeletePhoto(profile, photoId);
            return RedirectToAction("Index", "Home", new {name});
        }
    }
}