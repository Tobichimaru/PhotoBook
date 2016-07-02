using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository.ModelRepos
{
    public interface IProfileRepository : IRepository<DalProfile>
    {
        DalProfile GetProfileByName(string name);
        void DeletePhoto(DalProfile entity, int id);
        void AddLike(DalProfile profile, DalLike like);
        void RemoveLike(DalProfile profile, DalLike like);
    }
}