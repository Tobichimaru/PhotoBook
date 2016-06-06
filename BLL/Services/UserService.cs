using System.Collections.Generic;
using System.Linq;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            userRepository = repository;
        }

        public UserEntity GetById(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }

        public UserEntity GetUserByEmail(string email)
        {
            return userRepository.GetByEmail(email).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void Create(UserEntity user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }
    }
}