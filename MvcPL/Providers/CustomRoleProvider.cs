using System;
using System.Linq;
using System.Web.Security;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;

namespace MvcPL.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
        {
            get { return (IUserService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService)); }
        }

        public IRoleService RoleService
        {
            get { return (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService)); }
        }


        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string email, string roleName)
        {
            UserEntity user = UserService.GetAllEntities().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            RoleEntity userRole = RoleService.GetById(user.RoleId);
            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] {};
            var user = UserService.GetAllEntities().FirstOrDefault(u => u.Email == email);

            if (user == null) return roles;

            var role = RoleService.GetById(user.RoleId);
            return new string[] {role.Name};
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() { Name = roleName };
            RoleService.Create(newRole);
        }

        #region Stabs
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}