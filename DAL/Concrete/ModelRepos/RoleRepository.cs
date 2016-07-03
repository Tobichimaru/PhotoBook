using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
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

        public DalRole GetByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            ParameterExpression param = predicate.Parameters[0];
            BinaryExpression operation = (BinaryExpression)predicate.Body;
            MemberExpression left = (MemberExpression)operation.Left;
            ParameterExpression newParam = Expression.Parameter(typeof(Role), param.Name);
            MemberExpression prop = Expression.Property(newParam, left.Member.Name);
            BinaryExpression newOperation = Expression.MakeBinary(operation.NodeType, prop, operation.Right);
            Expression<Func<Role, bool>> func = Expression.Lambda<Func<Role, bool>>(newOperation, newParam);
            var role = _unitOfWork.Context.Set<Role>().FirstOrDefault(func);
            if (ReferenceEquals(role, null))
                return null;
            return role.ToDalRole();
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