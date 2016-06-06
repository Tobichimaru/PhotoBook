using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface
        public UserRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _unitOfWork.Context.Set<User>().Select(userEntity => new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId
            });
        }

        public DalUser GetById(int key)
        {
            var ormuser = _unitOfWork.Context.Set<User>().FirstOrDefault(user => user.UserId == key);
            if (!ReferenceEquals(ormuser, null))
                return ormuser.ToDalUser();
            return null;
        }

        public DalUser GetByEmail(string email)
        {
            var ormuser = _unitOfWork.Context.Set<User>().FirstOrDefault(user => user.Email == email);
            if (!ReferenceEquals(ormuser, null))
            {
                return ormuser.ToDalUser();
            }
            return null;
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            var user = e.ToOrmUser();
            user.UserProfile = _unitOfWork.Context.Set<Profile>().Find(e.ProfileId);
            user.Role = _unitOfWork.Context.Set<Role>().Find(e.RoleId);
            
            _unitOfWork.Context.Set<User>().Add(user);
            _unitOfWork.Commit();
        }

        public void Delete(DalUser e) //implement 
        {
            var user = _unitOfWork.Context.Set<User>().Single(u => u.UserId == e.Id);
            _unitOfWork.Context.Set<User>().Remove(user);
            _unitOfWork.Commit();
        }

        public void Update(DalUser entity)
        {
            throw new NotImplementedException();
        }
    }
}