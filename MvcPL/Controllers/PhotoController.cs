using System.Linq;
using System.Web.Mvc;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using MvcPL.Infrastructure.Mappers;

namespace MvcPL.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IPhotoService _service;

        public PhotoController(IPhotoService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePhoto(string name, int photoId)
        {
            var photo = _service.GetById(photoId);
            _service.Delete(photo);
            return RedirectToAction("Index", "Home", new { name });
        }

        public ActionResult Like(int photoId, string name)
        {
            var photo = _service.GetById(photoId);
            if (photo.Likes.FirstOrDefault(l => l.UserName == User.Identity.Name) != null)
            {
                _service.RemoveLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            else
            {
                _service.AddLike(new LikeEntity
                {
                    PhotoId = photoId,
                    UserName = User.Identity.Name
                });
            }
            photo = _service.GetById(photoId);
            return PartialView("Like", photo.ToMvcPhoto());
        }
	}
}