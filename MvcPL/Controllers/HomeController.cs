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

            foreach (var user in model)
            {
                foreach (var photo in user.Profile.Photos)
                {
                    photoList.Content.Add(photo.ToMvcPhoto());
                }
            }

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
    }
}