//Select Assemblies - > Extensions -> System.Web.Helpers

using System;
using System.Web.Helpers;
using System.Web.Security;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;


namespace MvcPL.Providers
{
    //провайдер членства помогает системе идентифицировать пользователя
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserRepository UserRepository
        {
            get { return (IUserRepository) System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository)); }
        }

        public IRoleRepository RoleRepository
        {
            get { return (IRoleRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleRepository)); }
        }

        public IProfileRepository ProfileRepository
        {
            get { return (IProfileRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IProfileRepository)); }
        }

        public MembershipUser CreateUser(string email, string name, string password)
        {
            DalUser user = new DalUser
            {
                Email = email,
                UserName = name,
                Password = Crypto.HashPassword(password),
                RoleId = RoleRepository.GetRoleByName("User").Id
                //http://msdn.microsoft.com/ru-ru/library/system.web.helpers.crypto(v=vs.111).aspx
            };


            UserRepository.Create(user);
            var membershipUser = GetUser(name, false);
            return membershipUser;
        }

        public override bool ValidateUser(string name, string password)
        {
            var user = UserRepository.GetByName(name);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                //Определяет, соответствуют ли заданный хэш RFC 2898 и пароль друг другу
            {
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string name, bool userIsOnline)
        {
            var user = UserRepository.GetByName(name);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.UserName,
                null, null, null, null,
                false, false, default(DateTime), 
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }


        public override string GetUserNameByEmail(string email)
        {
            var user = UserRepository.GetByEmail(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.UserName,
                null, null, null, null,
                false, false, default(DateTime),
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser.UserName;
        }


        #region Stabs

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}