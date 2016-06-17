using System.Security.Policy;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalEntityMappers
    {
        public static DalUser ToDalUser(this User userEntity)
        {
            var user = new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Profile = userEntity.UserProfile.ToDalProfile()
            };
            foreach (var item in userEntity.Photos)
            {
                user.Photos.Add(item.ToDalPhoto());
            }
            foreach (var item in userEntity.Likes)
            {
                user.Likes.Add(item.ToDalLike());
            }
            return user;
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            var user = new User
            {
                UserId = dalUser.Id,
                UserProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserProfile = dalUser.Profile.ToOrmProfile()
            };
            foreach (var item in dalUser.Photos)
            {
                user.Photos.Add(item.ToOrmPhoto());
            }
            foreach (var item in dalUser.Likes)
            {
                user.Likes.Add(item.ToOrmLike());
            }
            return user;
        }

        public static Profile ToOrmProfile(this DalProfile profile)
        {
            return new Profile
            {
                ProfileId = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar
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
                Avatar = profile.Avatar
            };
        }

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

        public static DalPhoto ToDalPhoto(this Photo orm)
        {
            var photo = new DalPhoto
            {
                Id = orm.PhotoId,
                CreatedOn = orm.CreatedOn,
                Description = orm.Description,
                Picture = orm.Picture,
                FullSize = orm.FullSize
            };
            foreach (var item in orm.Likes)
            {
                photo.Likes.Add(item.ToDalLike());
            }
            foreach (var item in orm.Tags)
            {
                photo.Tags.Add(item.ToDalTag());
            }
            return photo;
        }

        public static Photo ToOrmPhoto(this DalPhoto dal)
        {
            var photo = new Photo
            {
                PhotoId = dal.Id,
                CreatedOn = dal.CreatedOn,
                Description = dal.Description,
                Picture = dal.Picture,
                FullSize = dal.FullSize
            };
            foreach (var item in dal.Likes)
            {
                photo.Likes.Add(item.ToOrmLike());
            }
            foreach (var item in dal.Tags)
            {
                photo.Tags.Add(item.ToOrmTag());
            }
            return photo;
        }

        public static DalTag ToDalTag(this Tag tag)
        {
            return new DalTag
            {
                Id = tag.TagId,
                Name = tag.Name
            };
        }

        public static Tag ToOrmTag(this DalTag tag)
        {
            return new Tag
            {
                TagId = tag.Id,
                Name = tag.Name
            };
        }

        public static Like ToOrmLike(this DalLike like)
        {
            return new Like
            {
                LikeId = like.Id
            };
        }

        public static DalLike ToDalLike(this Like like)
        {
            return new DalLike
            {
                Id = like.LikeId
            };
        }
    }
}
