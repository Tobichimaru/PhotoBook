﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Interfacies.Repository.ModelRepos;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
         private readonly IUserRepository _repository;

         public HomeController(IUserRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var model = _repository.GetAll().Select(u => new UserViewModel
            {
                Email = u.Email
            });

            return View(model);
        }

        public ActionResult About()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.AuthType = User.Identity.AuthenticationType;
            }
            ViewBag.Login = User.Identity.Name;
            ViewBag.IsAdminInRole = User.IsInRole("Administrator")
                ? "You have administrator rights."
                : "You do not have administrator rights.";

            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersEdit()
        {
            var model = _repository.GetAll().Select(u => new UserViewModel
            {
                Email = u.Email
            });

            return View(model);
        }

        public ActionResult ShowGallery(string filter = null, int page = 1, int pageSize = 20)
        {
            var records = new PagedList<PhotoViewModel>();
            records.Content = new List<PhotoViewModel>();

            ViewBag.filter = filter;

            var ans = _repository.GetByEmail(User.Identity.Name).Photos
                .Where(x => filter == null || (x.Description.Contains(filter)))
                .OrderByDescending(x => x.Id)
                .Skip((page - 1)*pageSize)
                .Take(pageSize)
                .ToList();
            foreach (var item in ans)
            {
                records.Content.Add(item.ToMvcPhoto());   
            }

            // Count
            records.TotalRecords = _repository.GetByEmail(User.Identity.Name).Photos.Count(x => filter == null || (x.Description.Contains(filter)));

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);
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

            if (!files.Any() || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            var model = new PhotoViewModel();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Description = photo.Description;
                var fileName = Guid.NewGuid().ToString();
                var s = Path.GetExtension(file.FileName);
                if (s != null)
                {
                    var extension = s.ToLower();

                    using (var img = Image.FromStream(file.InputStream))
                    {
                        model.ThumbPath = String.Format("/GalleryImages/thumbs/{0}{1}", fileName, extension);
                        model.ImagePath = String.Format("/GalleryImages/{0}{1}", fileName, extension);
                        model.FullSize = imageToByteArray(img);

                        Size newSize = new Size(100, 100);
                        Size imgSize = NewImageSize(img.Size, newSize);
                        model.Picture = imageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));

                        newSize = new Size(600, 600);
                        imgSize = NewImageSize(img.Size, newSize);
                        model.FullSize = imageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));

                        // Save thumbnail size image, 100 x 100
                        SaveToFolder(img, fileName, extension, new Size(100, 100), model.ThumbPath);

                        // Save large size image, 800 x 800
                        SaveToFolder(img, fileName, extension, new Size(600, 600), model.ImagePath);
                    }
                }

                // Save record to database
                model.CreatedOn = DateTime.Now;
                var user = _repository.GetByEmail(User.Identity.Name);
                user.Photos.Add(model.ToDalPhoto());
                _repository.Update(user);
            }

            return RedirectPermanent("/Home/ShowGallery");
        }

        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }

        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }

        private byte[] imageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}