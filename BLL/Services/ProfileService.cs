using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepos;

namespace BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository repository)
        {
            this.uow = uow;
            profileRepository = repository;
        }

        public ProfileEntity GetById(int id)
        {
            return profileRepository.GetById(id).ToBllProfile();
        }

        public ProfileEntity GetProfileByName(string name)
        {
            return profileRepository.GetProfileByName(name).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllEntities()
        {
            return profileRepository.GetAll().Select(profile => profile.ToBllProfile());
        }

        public void Create(ProfileEntity profile)
        {
            profileRepository.Create(profile.ToDalProfile());
            uow.Commit();
        }

        public void Delete(ProfileEntity profile)
        {
            profileRepository.Delete(profile.ToDalProfile());
            uow.Commit();
        }

        public void Update(ProfileEntity item)
        {
            profileRepository.Update(item.ToDalProfile());
            uow.Commit();
        }
    }
}
