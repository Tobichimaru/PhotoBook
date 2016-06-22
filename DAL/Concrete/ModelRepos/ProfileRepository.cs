using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                Age = profile.Age,
                Avatar = profile.Avatar
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
            var orm = _unitOfWork.Context.Set<Profile>().FirstOrDefault(profile => profile.UserName == name);
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
            var profile = _unitOfWork.Context.Set<Profile>().First(p => p.UserName == entity.UserName);
            foreach (var photo in entity.Photos)
            {
                foreach (var tag in photo.Tags)
                {
                    if (_unitOfWork.Context.Set<Tag>().FirstOrDefault(p => p.Name == tag.Name) == null)
                    {
                        _unitOfWork.Context.Set<Tag>().Add(tag.ToOrmTag());
                    }
                }
                _unitOfWork.Commit();

                foreach (var tag in photo.Tags)
                {
                    var dbtag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(p => p.Name == tag.Name);
                    tag.Id = dbtag.TagId;
                }

                foreach (var like in photo.Likes)
                {
                    if (_unitOfWork.Context.Set<Like>().FirstOrDefault(p => p.PhotoId == like.PhotoId && p.UserName == profile.UserName) == null)
                    {
                        _unitOfWork.Context.Set<Like>().Add(like.ToOrmLike());
                    }
                }
                _unitOfWork.Commit();

                foreach (var like in photo.Likes)
                {
                    var dblike =
                        _unitOfWork.Context.Set<Like>()
                            .FirstOrDefault(p => p.PhotoId == like.PhotoId && p.UserName == profile.UserName);
                    like.Id = dblike.LikeId;
                }

                var dbphoto = _unitOfWork.Context.Set<Photo>().FirstOrDefault(p => p.PhotoId == photo.Id);
                if (dbphoto == null)
                {
                    _unitOfWork.Context.Set<Photo>().Add(photo.ToOrmPhoto());
                }

                if (profile.Photos.FirstOrDefault(p => p.PhotoId == photo.Id) == null)
                {
                    profile.Photos.Add(photo.ToOrmPhoto());
                }
            }

            if (entity.FirstName != null) profile.FirstName = entity.FirstName;
            if (entity.LastName != null) profile.LastName = entity.LastName;
            if (entity.Age != 0) profile.Age = entity.Age;
            if (entity.Avatar != null) profile.Avatar = entity.Avatar;

            _unitOfWork.Context.Set<Profile>().AddOrUpdate(profile);
            _unitOfWork.Commit();
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            throw new NotImplementedException();
        }

    }
}
