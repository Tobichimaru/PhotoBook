using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser
            {
                Id = userEntity.Id,
                ProfileId = userEntity.ProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId
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
                Password = dalUser.Password
            };
        }

        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            return new DalProfile
            {
                Id = profileEntity.Id,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Age = profileEntity.Age
            };
        }

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            return new ProfileEntity
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Age = dalProfile.Age
            };
        }

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
    }
}