using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalEntityMappers
    {
        public static DalUser ToDalUser(this User userEntity)
        {
            return new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId
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
                Password = dalUser.Password
            };
        }

        public static Profile ToOrmProfile(this DalProfile profile)
        {
            return new Profile
            {
                ProfileId = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                LastUpdateDate = profile.LastUpdateDate
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
                LastUpdateDate = profile.LastUpdateDate
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
    }
}
