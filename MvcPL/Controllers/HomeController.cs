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

    }
}