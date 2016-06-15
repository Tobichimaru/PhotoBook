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
    public class ProfileRepository : IProfileRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface
        public ProfileRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return _unitOfWork.Context.Set<Profile>().Select(profile => new DalProfile()
            {
                Id = profile.ProfileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age
            });
        }

        public DalProfile GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Profile>().FirstOrDefault(profile => profile.ProfileId == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalProfile();
            return null;
        }

        public DalProfile GetProfileByName(string name)
        {
            var orm = _unitOfWork.Context.Set<Profile>().FirstOrDefault(profile => profile.FirstName == name);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalProfile();
            }
            return null;
        }

        public void Create(DalProfile e)
        {
            var profile = e.ToOrmProfile();
            _unitOfWork.Context.Set<Profile>().Add(profile);
            _unitOfWork.Commit();
        }

        public void Delete(DalProfile e)
        {
            var profile = _unitOfWork.Context.Set<Profile>().Single(u => u.ProfileId == e.Id);
            _unitOfWork.Context.Set<Profile>().Remove(profile);
            _unitOfWork.Commit();
        }

        public void Update(DalProfile entity)
        {
            Delete(GetById(entity.Id));
            Create(entity);
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            throw new NotImplementedException();
        }

    }
}
