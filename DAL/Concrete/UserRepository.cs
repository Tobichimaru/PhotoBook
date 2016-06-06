using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using ORM.Models;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            context = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Select(user => new DalUser
            {
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId,
                ProfileId = user.UserProfileId
            });
        }

        public DalUser GetById(int key)
        {
            var ormuser = context.Set<User>().FirstOrDefault(user => user.UserId == key);
            if (!ReferenceEquals(ormuser, null))
                return new DalUser
                {
                    Id = ormuser.UserId,
                    Email = ormuser.Email,
                    Password = ormuser.Password,
                    RoleId = ormuser.RoleId,
                    ProfileId = ormuser.UserProfileId
                };
            return new DalUser();
        }

        public DalUser GetByEmail(string email)
        {
            var ormuser = context.Set<User>().FirstOrDefault(user => user.Email == email);
            if (!ReferenceEquals(ormuser, null))
            {
                var user = new DalUser
                {
                    Id = ormuser.UserId,
                    Email = ormuser.Email,
                    Password = ormuser.Password,
                    //RolesIds = new[] { ormuser.Roles.GetEnumerator().Current.RoleId }, //TO DO: replace
                    ProfileId = ormuser.UserProfileId
                };
                return user;
            }
            return new DalUser();
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            Debug.WriteLine("UserRepository");
            var user = new User
            {
                UserId = e.Id,
                Email = e.Email,
                Password = e.Password,
                UserProfileId = e.ProfileId,
                RoleId = e.RoleId,
                Role = context.Set<Role>().Find(e.RoleId)
            };
            var profile = new Profile
            {
                LastUpdateDate = DateTime.Now
            };
            context.Set<Profile>().Add(profile);
            context.SaveChanges();

            user.UserProfile = profile;
            user.UserProfileId = profile.ProfileId;
            context.Set<User>().Add(user);
            context.SaveChanges();
        }

        public void Delete(DalUser e)
        {
            var user = context.Set<User>().Single(u => u.UserId == e.Id);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }

        
    }
}