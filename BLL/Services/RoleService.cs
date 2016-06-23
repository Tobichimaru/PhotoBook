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
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            roleRepository = repository;
        }

        public RoleEntity GetById(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<RoleEntity> GetAllEntities()
        {
            return roleRepository.GetAll().Select(role => role.ToBllRole());
        }

        public void Create(RoleEntity role)
        {
            roleRepository.Create(role.ToBllRole());
            uow.Commit();
        }

        public void Delete(RoleEntity role)
        {
            roleRepository.Delete(role.ToBllRole());
            uow.Commit();
        }

        public void Update(RoleEntity item)
        {
            roleRepository.Update(item.ToBllRole());
            uow.Commit();
        }

        public RoleEntity GetRoleByName(string name)
        {
            return roleRepository.GetRoleByName(name).ToBllRole();
        }
    }
}
