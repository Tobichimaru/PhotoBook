using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
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
            Profile profile;
            profile = e.Profile == null ? new Profile() : e.Profile.ToOrmProfile();
            if ( _unitOfWork.Context.Set<Profile>().Find(e.ProfileId) == null)
            {
                _unitOfWork.Context.Set<Profile>().Add(profile);
            }
            e.ProfileId = profile.ProfileId;
            e.Profile = profile.ToDalProfile();

            var user = e.ToOrmUser();
            user.Role = _unitOfWork.Context.Set<Role>().Find(e.RoleId);

            _unitOfWork.Context.Set<User>().Add(user);
            _unitOfWork.Commit();
        }

        public void Delete(DalUser e) //implement 
        {
            var user = _unitOfWork.Context.Set<User>().Single(u => u.UserId == e.Id);
            //foreach (var photo in user.Photos)
            //{
            //    _unitOfWork.Context.Set<Photo>().Remove(photo);
            //}
            //foreach (var like in user.Likes)
            //{
            //    _unitOfWork.Context.Set<Like>().Remove(like);
            //}
            _unitOfWork.Context.Set<Profile>().Remove(user.UserProfile);
            _unitOfWork.Context.Set<User>().Remove(user);
            _unitOfWork.Commit();
        }

        public void Update(DalUser entity)
        {
            Delete(GetById(entity.Id));
            Create(entity);
        }
    }
}