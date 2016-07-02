using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IProfileService : IService<ProfileEntity>
    {
        ProfileEntity GetProfileByName(string name);
        void DeletePhoto(ProfileEntity entity, int id);
        void RemoveLike(ProfileEntity profile, LikeEntity likeEntity);
        void AddLike(ProfileEntity profile, LikeEntity likeEntity);
    }
}