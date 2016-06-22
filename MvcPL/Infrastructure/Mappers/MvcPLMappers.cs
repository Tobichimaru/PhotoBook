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

        public static ICollection<DalTag> ToDalTags(this ICollection<TagModel> tags)
        {
            var result = new List<DalTag>();
            foreach (var tag in tags)
            {
                result.Add(new DalTag
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        public static ICollection<TagModel> ToMvcTags(this ICollection<DalTag> tags)
        {
            var result = new List<TagModel>();
            foreach (var tag in tags)
            {
                result.Add(new TagModel
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        #endregion

        #region Like

        public static ICollection<DalLike> ToDalLikes(this ICollection<LikeModel> likes)
        {
            var result = new List<DalLike>();
            foreach (var like in likes)
            {
                result.Add(new DalLike
                {
                    Id = like.Id,
                    UserName = like.UserName,
                    PhotoId = like.PhotoId
                });
            }
            return result;
        }

        public static ICollection<LikeModel> ToMvcLikes(this ICollection<DalLike> likes)
        {
            var result = new List<LikeModel>();
            foreach (var like in likes)
            {
                result.Add(new LikeModel
                {
                    Id = like.Id,
                    UserName = like.UserName,
                    PhotoId = like.PhotoId
                });
            }
            return result;
        }

        #endregion

        #region Photo
        public static DalPhoto ToDalPhoto(this PhotoViewModel photo)
        {
            return new DalPhoto
            {
                Id = photo.Id,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                Tags = photo.Tags.ToDalTags(),
                Likes = photo.Likes.ToDalLikes()
            };
        }

        public static PhotoViewModel ToMvcPhoto(this DalPhoto photo, string name)
        {
            return new PhotoViewModel
            {
                Id = photo.Id,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                UserName = name,
                Tags = photo.Tags.ToMvcTags(),
                Likes = new List<LikeModel>(photo.Likes.ToMvcLikes())
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