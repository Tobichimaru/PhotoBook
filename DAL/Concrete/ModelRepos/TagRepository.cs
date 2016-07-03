using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
{
    public class TagRepository : ITagRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface

        public TagRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalTag> GetAll()
        {
            return _unitOfWork.Context.Set<Tag>().Select(tag => new DalTag
            {
                Id = tag.TagId,
                Name = tag.Name,
                Photos = tag.Photos.Select(photo => new DalPhoto
                {
                    Id = photo.PhotoId,
                    CreatedOn = photo.CreatedOn,
                    Picture = photo.Picture,
                    FullSize = photo.FullSize,
                    ProfileId = photo.ProfileId,
                    Description = photo.Description,
                    Likes = photo.Likes.Select(l => new DalLike
                    {
                        Id = l.LikeId,
                        PhotoId = l.PhotoId,
                        UserName = l.UserName
                    }).ToList(),
                    Tags = photo.Tags.Select(t => new DalTag
                    {
                        Id = t.TagId,
                        Name = t.Name
                    }).ToList()
                }).ToList()
            });
        }

        public DalTag GetById(int key)
        {
            var ormtag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(tag => tag.TagId == key);
            if (!ReferenceEquals(ormtag, null))
                return ormtag.ToDalTag();
            return null;
        }

        public DalTag GetByPredicate(Expression<Func<DalTag, bool>> predicate)
        {
            ParameterExpression param = predicate.Parameters[0];
            BinaryExpression operation = (BinaryExpression)predicate.Body;
            MemberExpression left = (MemberExpression)operation.Left;
            ParameterExpression newParam = Expression.Parameter(typeof(Tag), param.Name);
            MemberExpression prop = Expression.Property(newParam, left.Member.Name);
            BinaryExpression newOperation = Expression.MakeBinary(operation.NodeType, prop, operation.Right);
            Expression<Func<Tag, bool>> func = Expression.Lambda<Func<Tag, bool>>(newOperation, newParam);
            var tag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(func);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToDalTag();
        }

        public void Create(DalTag e)
        {
            var tag = e.ToOrmTag();
            _unitOfWork.Context.Set<Tag>().Add(tag);
            _unitOfWork.Commit();
        }

        public void Delete(DalTag e)
        {
            var tag = _unitOfWork.Context.Set<Tag>().Single(u => u.TagId == e.Id);
            _unitOfWork.Context.Set<Tag>().Remove(tag);
            _unitOfWork.Commit();
        }

        public void Update(DalTag entity)
        {
            foreach (var photo in entity.Photos)
            {
                _unitOfWork.Context.Set<Photo>().AddOrUpdate(photo.ToOrmPhoto());
            }
            _unitOfWork.Context.Set<Tag>().AddOrUpdate(entity.ToOrmTag());
            _unitOfWork.Commit();
        }

        public DalTag GetTagByName(string name)
        {
            var ormtag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(tag => tag.Name == name);
            if (!ReferenceEquals(ormtag, null))
                return ormtag.ToDalTag();
            return null;
        }
    }
}
