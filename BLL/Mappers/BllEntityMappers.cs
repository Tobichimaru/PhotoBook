using System.Collections.Generic;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region User
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser
            {
                Id = userEntity.Id,
                ProfileId = userEntity.ProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Profile = userEntity.Profile.ToDalProfile(),
                UserName = userEntity.UserName
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Profile = dalUser.Profile.ToBllProfile(),
                UserName = dalUser.UserName
            };
        }
        #endregion

        #region Profile
        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            return new DalProfile
            {
                Id = profileEntity.Id,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Age = profileEntity.Age,
                Avatar = profileEntity.Avatar,
                Photos = profileEntity.Photos.ToDalPhotos(),
                UserName = profileEntity.UserName
            };
        }

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            return new ProfileEntity
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Age = dalProfile.Age,
                Avatar = dalProfile.Avatar,
                UserName = dalProfile.UserName,
                Photos = dalProfile.Photos.ToBllPhotos()
            };
        }
        #endregion

        #region Like

        public static LikeEntity ToBllLike(this DalLike like)
        {
            return new LikeEntity
            {
                Id = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static DalLike ToDalLike(this LikeEntity like)
        {
            return new DalLike
            {
                Id = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static ICollection<LikeEntity> ToBllLikes(this ICollection<DalLike> likes)
        {
            var result = new List<LikeEntity>();
            foreach (var like in likes)
            {
                result.Add(like.ToBllLike());
            }
            return result;
        }

        public static ICollection<DalLike> ToDalLikes(this ICollection<LikeEntity> likes)
        {
            var result = new List<DalLike>();
            foreach (var like in likes)
            {
                result.Add(like.ToDalLike());
            }
            return result;
        }
        #endregion

        #region Tag

        public static TagEntity ToBllTag(this DalTag tag)
        {
            return new TagEntity
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static DalTag ToDalTag(this TagEntity tag)
        {
            return new DalTag
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }

        public static ICollection<TagEntity> ToBllTags(this ICollection<DalTag> tags)
        {
            var result = new List<TagEntity>();
            foreach (var tag in tags)
            {
                result.Add(tag.ToBllTag());
            }
            return result;
        }

        public static ICollection<DalTag> ToDalTags(this ICollection<TagEntity> tags)
        {
            var result = new List<DalTag>();
            foreach (var tag in tags)
            {
                result.Add(tag.ToDalTag());
            }
            return result;
        }
        #endregion

        #region Photo

        public static PhotoEntity ToBllPhoto(this DalPhoto photo)
        {
            return new PhotoEntity
            {
                CreatedOn = photo.CreatedOn,
                FullSize = photo.FullSize,
                Id = photo.Id,
                Likes = photo.Likes.ToBllLikes(),
                Picture = photo.Picture,
                Tags = photo.Tags.ToBllTags()
            };
        }

        public static DalPhoto ToDalPhoto(this PhotoEntity photo)
        {
            return new DalPhoto()
            {
                CreatedOn = photo.CreatedOn,
                FullSize = photo.FullSize,
                Id = photo.Id,
                Likes = photo.Likes.ToDalLikes(),
                Picture = photo.Picture,
                Tags = photo.Tags.ToDalTags()
            };
        }

        public static ICollection<PhotoEntity> ToBllPhotos(this ICollection<DalPhoto> photos)
        {
            var result = new List<PhotoEntity>();
            foreach (var photo in photos)
            {
                result.Add(photo.ToBllPhoto());
            }
            return result;
        }

        public static ICollection<DalPhoto> ToDalPhotos(this ICollection<PhotoEntity> photos)
        {
            var result = new List<DalPhoto>();
            foreach (var photo in photos)
            {
                result.Add(photo.ToDalPhoto());
            }
            return result;
        }

        #endregion

        #region Role
        public static RoleEntity ToBllRole(this DalRole role)
        {
            var roleEntity = new RoleEntity
            {
                Id = role.Id,
                Name = role.Name
            };
            foreach (var item in role.Users)
            {
                roleEntity.Users.Add(item.ToBllUser());
            }
            return roleEntity;
        }

        public static DalRole ToBllRole(this RoleEntity role)
        {
            var dalRole = new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
            foreach (var item in role.Users)
            {
                dalRole.Users.Add(item.ToDalUser());
            }
            return dalRole;
        }
        #endregion
    }
}