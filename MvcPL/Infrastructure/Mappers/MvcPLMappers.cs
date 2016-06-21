using System.Collections.Generic;
using System.Text;
using DAL.Interfacies.DTO;
using MvcPL.Models;
using MvcPL.Models.Photo;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        #region User
        public static UserViewModel ToMvcUser(this DalUser user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.UserName,
                Id = user.Id
            };
        }

        public static DalUser ToDalUser(this UserViewModel userViewModel)
        {
            return new DalUser
            {
                Email = userViewModel.Email,
                UserName = userViewModel.Name,
                Id = userViewModel.Id
            };
        }
        #endregion

        #region Tag

        public static ICollection<DalTag> ToDalTags(this string tags)
        {
            var names = tags.Split(' ');
            ICollection<DalTag> newTags = new List<DalTag>();
            foreach (var name in names)
            {
                newTags.Add(new DalTag
                {
                    Name = name
                });
            }
            return newTags;
        }

        public static string ToMvcTags(this ICollection<DalTag> tags)
        {
            StringBuilder newTags = new StringBuilder();
            foreach (var tag in tags)
            {
                newTags.Append(tag.Name + " ");
            }
            return newTags.ToString();
        }

        #endregion

        #region Photo
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
                //Tags = photo.Tags.ToDalTags()
            };
        }

        public static PhotoViewModel ToMvcPhoto(this DalPhoto photo, string name)
        {
            return new PhotoViewModel
            {
                CreatedOn = photo.CreatedOn,
                Description = photo.Description,
                ImagePath = photo.ImagePath,
                ThumbPath = photo.ThumbPath,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                UserName = name
                //Tags = photo.Tags.ToMvcTags()
            };
        }
        #endregion

        #region Profile
        public static DalProfile ToDalProfile(this ProfileViewModel model)
        {
            return new DalProfile
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = model.Avatar,
                UserName = model.UserName
            };
        }
        
        public static ProfileViewModel ToMvcProfile(this DalProfile model)
        {
            return new ProfileViewModel
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = model.Avatar,
                UserName = model.UserName
            };
        }
        #endregion

    }
}