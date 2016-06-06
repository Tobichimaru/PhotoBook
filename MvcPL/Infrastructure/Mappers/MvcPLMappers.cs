using System;
using BLL.Interfacies.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel
            {
                Email = userEntity.Email
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            var user = new UserEntity
            {
                Email = userViewModel.Email,
                RoleId = userViewModel.RoleId
            };
            return user;
        }
    }
}