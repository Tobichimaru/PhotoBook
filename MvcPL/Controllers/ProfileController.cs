using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfacies.Repository.ModelRepos;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.Photo;

namespace MvcPL.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _repository;

        public ProfileController(IProfileRepository repository)
        {
            _repository = repository;
        }

        [Route("user/{name}")]
        public ActionResult UserPage(string name)
        {
            var profile = _repository.GetProfileByName(name);
            if (profile == null)
            {
                return View("Error");
            }

            var records = new PagedList<PhotoViewModel>();
            records.Content = new List<PhotoViewModel>();
            foreach (var item in _repository.GetProfileByName(name).Photos)
            {
                records.Content.Add(item.ToMvcPhoto(null));
            }

            // Count
            records.TotalRecords = _repository.GetProfileByName(name).Photos.Count;
            records.CurrentPage = 1;
            records.PageSize = 12;

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
            var profile = _repository.GetProfileByName(User.Identity.Name);
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream target = new MemoryStream();
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

            _repository.Update(profile);
            return RedirectToAction("UserPage", new {name = profile.UserName});
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            var photo = new PhotoViewModel();
            return View(photo);
        }

        [HttpPost]
        public ActionResult Create(PhotoViewModel photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(photo);

            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();
            if (!httpPostedFileBases.Any() || httpPostedFileBases.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            var model = new PhotoViewModel();
            var profile = _repository.GetProfileByName(User.Identity.Name);

            foreach (var file in httpPostedFileBases)
            {
                if (file.ContentLength == 0) continue;

                model.Description = photo.Description;
                using (var img = Image.FromStream(file.InputStream))
                {
                    Image cutImage = GalleryHelper.CutImage(img, 300, 300);
                    model.Picture = GalleryHelper.ImageToByteArray(cutImage);

                    var newSize = new Size(600, 600);
                    var imgSize = GalleryHelper.NewImageSize(img.Size, newSize);
                    model.FullSize = GalleryHelper.ImageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));
                }
                // Save record to database
                model.CreatedOn = DateTime.Now;
                profile.Photos.Add(model.ToDalPhoto());
            }
            _repository.Update(profile);
            return RedirectToAction("UserPage", new {name = profile.UserName});
        }
	}
}