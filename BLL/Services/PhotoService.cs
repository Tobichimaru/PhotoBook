using System;
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
        private readonly IPhotoRepository photoRepository;
        private readonly IUnitOfWork uow;

        public PhotoService(IUnitOfWork uow, IPhotoRepository repository)
        {
            this.uow = uow;
            photoRepository = repository;
        }

        public PhotoEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var photo = photoRepository.GetById(id);
            if (ReferenceEquals(photo, null))
                return null;
            return photo.ToBllPhoto();
        }

        public IEnumerable<PhotoEntity> GetAllEntities()
        {
            return photoRepository.GetAll().Select(p => p.ToBllPhoto());
        }

        public void Create(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Create(item.ToDalPhoto());
            uow.Commit();
        }

        public void Delete(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Delete(item.ToDalPhoto());
            uow.Commit();
        }

        public void Update(PhotoEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            photoRepository.Update(item.ToDalPhoto());
            uow.Commit();
        }

        public void RemoveLike(LikeEntity likeEntity)
        {
            if (ReferenceEquals(likeEntity, null))
                throw new ArgumentNullException();

            photoRepository.RemoveLike(likeEntity.ToDalLike());
            uow.Commit();
        }

        public void AddLike(LikeEntity likeEntity)
        {
            if (ReferenceEquals(likeEntity, null))
                throw new ArgumentNullException();

            photoRepository.AddLike(likeEntity.ToDalLike());
            uow.Commit();
        }
    }
}