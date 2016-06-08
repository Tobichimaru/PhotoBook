using System;
using System.Linq;
using System.Web.Security;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;

namespace MvcPL.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserRepository UserRepository
        {
            get { return (IUserRepository) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository)); }
        }

        public IRoleRepository RoleRepository
        {
            get { return (IRoleRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleRepository)); }
        }


        public override string ApplicationName { get; set; }

        public override bool IsUserInRole(string email, string roleName)
        {
            DalUser user = UserRepository.GetAll().FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            DalRole userRole = RoleRepository.GetById(user.RoleId);
            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] {};
            var user = UserRepository.GetAll().FirstOrDefault(u => u.Email == email);

            if (user == null) return roles;

            var role = RoleRepository.GetById(user.RoleId);
            return new string[] {role.Name};
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new DalRole { Name = roleName };
            RoleRepository.Create(newRole);
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