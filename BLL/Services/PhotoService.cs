using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepos;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork uow;
        private readonly IPhotoRepository photoRepository;

        public PhotoService(IUnitOfWork uow, IPhotoRepository repository)
        {
            this.uow = uow;
            photoRepository = repository;
        }

        public PhotoEntity GetById(int id)
        {
            return photoRepository.GetById(id).ToBllPhoto();
        }

        public IEnumerable<PhotoEntity> GetAllEntities()
        {
            return photoRepository.GetAll().Select(p => p.ToBllPhoto());
        }

        public void Create(PhotoEntity item)
        {
            photoRepository.Create(item.ToDalPhoto());
            uow.Commit();
        }

        public void Delete(PhotoEntity item)
        {
            photoRepository.Delete(item.ToDalPhoto());
            uow.Commit();
        }

        public void Update(PhotoEntity item)
        {
            photoRepository.Update(item.ToDalPhoto());
            uow.Commit();
        }
    }
}
