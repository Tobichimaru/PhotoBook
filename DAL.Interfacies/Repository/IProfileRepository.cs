using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IProfileRepository : IRepository<DalProfile>
    {
        DalProfile GetProfileByName(string name);
    }
}
