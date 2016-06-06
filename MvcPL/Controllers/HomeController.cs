using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Services;
using MvcPL.Models;

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

        public ActionResult Index()
        {
            var model = _Service.GetAllEntities().Select(u => new UserViewModel
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
            //HttpContext.Profile["FirstName"] = "Вася";
            //HttpContext.Profile["LastName"] = "Иванов";
            //HttpContext.Profile.SetPropertyValue("Age",23);
            //Response.Write(HttpContext.Profile.GetPropertyValue("FirstName"));
            //Response.Write(HttpContext.Profile.GetPropertyValue("LastName"));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UsersEdit()
        {
            var model = _Service.GetAllEntities().Select(u => new UserViewModel
            {
                Email = u.Email
            });

            return View(model);
        }
    }
}