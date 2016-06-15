using DAL.Interfacies.DTO;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this DalUser user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.Name,
                Id = user.Id
            };
        }

        public static DalUser ToDalUser(this UserViewModel userViewModel)
        {
            return new DalUser
            {
                Email = userViewModel.Email,
                Name = userViewModel.Name,
                Id = userViewModel.Id
            };
        }

        public static DalPhoto ToDalPhoto(this PhotoViewModel photo)
        {
            return new DalPhoto
            {
                CreatedOn = photo.CreatedOn,
                Description = photo.Description,
                ImagePath = photo.ImagePath,
                ThumbPath = photo.ThumbPath,
                Picture = photo.Picture,
                FullSize = photo.FullSize
            };
        }

        public static PhotoViewModel ToMvcPhoto(this DalPhoto photo)
        {
            return new PhotoViewModel
            {
                CreatedOn = photo.CreatedOn,
                Description = photo.Description,
                ImagePath = photo.ImagePath,
                ThumbPath = photo.ThumbPath,
                Picture = photo.Picture,
                FullSize = photo.FullSize
            };
        }

        public static DalProfile ToDalProfile(this ProfileViewModel model)
        {
            return new DalProfile
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static ProfileViewModel ToMvcProfile(this DalProfile model)
        {
            return new ProfileViewModel
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }
    }
}