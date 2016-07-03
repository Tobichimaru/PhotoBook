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
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork uow;

        public TagService(IUnitOfWork uow, ITagRepository repository)
        {
            this.uow = uow;
            tagRepository = repository;
        }


        public TagEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var tag = tagRepository.GetById(id);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllEntities()
        {
            return tagRepository.GetAll().Select(tag => tag.ToBllTag());
        }

        public void Create(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Create(item.ToDalTag());
            uow.Commit();
        }

        public void Delete(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Delete(item.ToDalTag());
            uow.Commit();
        }

        public void Update(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Update(item.ToDalTag());
            uow.Commit();
        }

        public TagEntity GetTagByName(string name)
        {
            var tag = tagRepository.GetTagByName(name);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToBllTag();
        }
    }
}
