using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface
        public RoleRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _unitOfWork.Context.Set<Role>().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Role>().FirstOrDefault(role => role.RoleId == key);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalRole();
            }
            return null;
        }

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Create(DalRole e)
        {
            var role = e.ToOrmRole();
            _unitOfWork.Context.Set<Role>().Add(role);
            _unitOfWork.Commit();
        }

        public void Delete(DalRole e)
        {
            var role = _unitOfWork.Context.Set<Role>().Single(u => u.RoleId == e.Id);
            _unitOfWork.Context.Set<Role>().Remove(role);
            _unitOfWork.Commit();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public DalRole GetRoleByName(string name)
        {
            var orm = _unitOfWork.Context.Set<Role>().FirstOrDefault(role => role.Name == name);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalRole();
            }
            return null;
        }
    }
}
