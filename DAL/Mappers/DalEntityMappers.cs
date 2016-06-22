using System.CodeDom;
using System.Collections.Generic;
using System.Security.Policy;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalEntityMappers
    {
        #region User
        public static DalUser ToDalUser(this User userEntity)
        {
            return new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Profile = userEntity.UserProfile.ToDalProfile(),
                UserName = userEntity.UserName
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User
            {
                UserId = dalUser.Id,
                UserProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserProfile = dalUser.Profile.ToOrmProfile(),
                UserName = dalUser.UserName
            };
        }
        #endregion

        #region Profile
        public static Profile ToOrmProfile(this DalProfile profile)
        {
            return new Profile
            {
                ProfileId = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar,
                UserName = profile.UserName,
                Photos = profile.Photos.ToOrmPhotos()
            };
        }

        public static DalProfile ToDalProfile(this Profile profile)
        {
            return new DalProfile
            {
                Id = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar,
                UserName = profile.UserName,
                Photos = profile.Photos.ToDalPhotos()
            };
        }
        #endregion

        #region Role
        public static Role ToOrmRole(this DalRole role)
        {
            var orm = new Role
            {
                RoleId = role.Id,
                Name = role.Name,
            };
            foreach (var item in role.Users)
            {
                orm.Users.Add(item.ToOrmUser());   
            }
            return orm;
        }

        public static DalRole ToDalRole(this Role orm)
        {
            var role = new DalRole
            {
                Id = orm.RoleId,
                Name = orm.Name
            };
            foreach (var item in orm.Users)
            {
                role.Users.Add(item.ToDalUser());
            }
            return role;
        }
        #endregion

        #region Photo
        public static DalPhoto ToDalPhoto(this Photo orm)
        {
            return new DalPhoto
            {
                Id = orm.PhotoId,
                CreatedOn = orm.CreatedOn,
                Picture = orm.Picture,
                FullSize = orm.FullSize,
                Likes = orm.Likes.ToDalLikes(),
                Tags = orm.Tags.ToDalTags()
            };
        }

        public static Photo ToOrmPhoto(this DalPhoto dal)
        {
            return new Photo
            {
                PhotoId = dal.Id,
                CreatedOn = dal.CreatedOn,
                Picture = dal.Picture,
                FullSize = dal.FullSize,
                Likes = dal.Likes.ToOrmLikes(),
                Tags = dal.Tags.ToOrmTags()
            };
        }

        public static ICollection<DalPhoto> ToDalPhotos(this ICollection<Photo> photos)
        {
            ICollection<DalPhoto> newPhotos = new List<DalPhoto>();
            foreach (var photo in photos)
            {
                newPhotos.Add(photo.ToDalPhoto());
            }
            return newPhotos;
        }

        public static ICollection<Photo> ToOrmPhotos(this ICollection<DalPhoto> photos)
        {
            ICollection<Photo> newPhotos = new List<Photo>();
            foreach (var photo in photos)
            {
                newPhotos.Add(photo.ToOrmPhoto());
            }
            return newPhotos;
        }
        #endregion

        #region Tag
        public static DalTag ToDalTag(this Tag tag)
        {
            return new DalTag
            {
                Id = tag.TagId,
                Name = tag.Name
            };
        }

        public static ICollection<DalTag> ToDalTags(this ICollection<Tag> tags)
        {
            ICollection<DalTag> newTags = new List<DalTag>();
            foreach (var tag in tags)
            {
                newTags.Add(tag.ToDalTag());
            }
            return newTags;
        }

        public static ICollection<Tag> ToOrmTags(this ICollection<DalTag> tags)
        {
            ICollection<Tag> newTags = new List<Tag>();
            foreach (var tag in tags)
            {
                newTags.Add(tag.ToOrmTag());
            }
            return newTags;
        }

        public static Tag ToOrmTag(this DalTag tag)
        {
            return new Tag
            {
                TagId = tag.Id,
                Name = tag.Name
            };
        }
        #endregion

        #region Like
        public static Like ToOrmLike(this DalLike like)
        {
            return new Like
            {
                LikeId = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static DalLike ToDalLike(this Like like)
        {
            return new DalLike
            {
                Id = like.LikeId,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static ICollection<DalLike> ToDalLikes(this ICollection<Like> likes)
        {
            ICollection<DalLike> newLikes = new List<DalLike>();
            foreach (var like in likes)
            {
                newLikes.Add(like.ToDalLike());
            }
            return newLikes;
        }


        public static ICollection<Like> ToOrmLikes(this ICollection<DalLike> likes)
        {
            ICollection<Like> newLikes = new List<Like>();
            foreach (var like in likes)
            {
                newLikes.Add(like.ToOrmLike());
            }
            return newLikes;
        }
        #endregion
    }
}
