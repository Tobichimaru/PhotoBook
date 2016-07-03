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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork uow;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            roleRepository = repository;
        }

        public RoleEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public void Create(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException();

            roleRepository.Create(role.ToBllRole());
            uow.Commit();
        }

        public void Delete(RoleEntity role)
        {
            if (ReferenceEquals(role, null))
                throw new ArgumentNullException();

            roleRepository.Delete(role.ToBllRole());
            uow.Commit();
        }

        public void Update(RoleEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            roleRepository.Update(item.ToBllRole());
            uow.Commit();
        }

        public RoleEntity GetRoleByName(string name)
        {
            var role = roleRepository.GetRoleByName(name);
            if (ReferenceEquals(role, null))
                return null;
            return role.ToBllRole();
        }
    }
}