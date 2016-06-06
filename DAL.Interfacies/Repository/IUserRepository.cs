using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IUserRepository : IRepository<DalUser> //Add user repository methods!
    {
        DalUser GetByEmail(string email);
    }
}