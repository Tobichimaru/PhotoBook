using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IRoleRepository : IRepository<DalRole>
    {
        DalRole GetRoleByName(string name);
    }
}
