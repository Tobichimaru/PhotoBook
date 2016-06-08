﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface
        public PhotoRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalPhoto> GetAll()
        {
            return _unitOfWork.Context.Set<Photo>().Select(photo => photo.ToDalPhoto());
        }

        public DalPhoto GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Photo>().FirstOrDefault(photo => photo.PhotoId == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalPhoto();
            return null;
        }

        public DalPhoto GetPhotoByPath(string path)
        {
            var orm = _unitOfWork.Context.Set<Photo>().FirstOrDefault(photo => photo.ImagePath == path);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalPhoto();
            }
            return null;
        }

        public void Create(DalPhoto e)
        {
            var photo = e.ToOrmPhoto();
            _unitOfWork.Context.Set<Photo>().Add(photo);
            _unitOfWork.Commit();
        }

        public void Delete(DalPhoto e)
        {
            var photo = _unitOfWork.Context.Set<Photo>().Single(u => u.PhotoId == e.Id);
            _unitOfWork.Context.Set<Photo>().Remove(photo);
            _unitOfWork.Commit();
        }

        public void Update(DalPhoto entity)
        {
            throw new NotImplementedException();
        }

        public DalPhoto GetByPredicate(Expression<Func<DalPhoto, bool>> f)
        {
            throw new NotImplementedException();
        }

    }
}