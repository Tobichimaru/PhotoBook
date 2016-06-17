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
            var model = _repository.GetAll();
            PagedList<PhotoViewModel> photoList = new PagedList<PhotoViewModel>();
            photoList.Content = new List<PhotoViewModel>();
            model.ToList().ForEach(i => i.Photos.ToList().ForEach(p => photoList.Content.Add(p.ToMvcPhoto())));
            photoList.Content.Sort((viewModel, photoViewModel) => -viewModel.CreatedOn.CompareTo(photoViewModel.CreatedOn));
            photoList.PageSize = 20;
            photoList.CurrentPage = 1;

            return View(photoList);
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

        public ActionResult UserPage()
        {
            var model = new UserPageModel
            {
                photos = ShowGallery(),
                profile = _repository.GetByEmail(User.Identity.Name).Profile.ToMvcProfile()
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
            var user = _repository.GetByEmail(User.Identity.Name);
            var profile = user.Profile;
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
                profile.Avatar = imageToByteArray(CutImage(image, 150, 150));
            }

            profile.FirstName = viewModel.FirstName ?? profile.FirstName;
            profile.LastName = viewModel.LastName ?? profile.LastName;
            profile.Age = viewModel.Age == 0? profile.Age : viewModel.Age;
            user.Profile = profile;

            _repository.Update(user);
            return RedirectPermanent("/Home/UserPage");
        }

        public PagedList<PhotoViewModel> ShowGallery(string filter = null, int page = 1, int pageSize = 20)
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
            return records;
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
                using (var img = Image.FromStream(file.InputStream))
                {
                    Image cutImage = CutImage(img, 300, 300);
                    model.Picture = imageToByteArray(cutImage);

                    var newSize = new Size(600, 600);
                    var imgSize = NewImageSize(img.Size, newSize);
                    model.FullSize = imageToByteArray(new Bitmap(img, imgSize.Width, imgSize.Height));
                }

                // Save record to database
                model.CreatedOn = DateTime.Now;
                var user = _repository.GetByEmail(User.Identity.Name);
                user.Photos.Add(model.ToDalPhoto());
                _repository.Update(user);
            }

            return RedirectPermanent("/Home/UserPage");
        }

        public Image CutImage(Image target, int width, int height)
        {
            Bitmap bmpImage = new Bitmap(target);
            int size = Math.Min(target.Width, target.Height);
            var rect = new Rectangle((target.Width - size)/2, (target.Height - size)/2, size, size);
            var img = bmpImage.Clone(rect, bmpImage.PixelFormat);

            var newSize = new Size(width, height);
            var imgSize = NewImageSize(img.Size, newSize);
            return new Bitmap(img, imgSize.Width, imgSize.Height);
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