using System.Collections.Generic;
namespace BLL.Interfacies.Services
{
    public interface IService<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAllEntities();
        void Create(T item);
        void Delete(T item);
        void Update(T item);
    }
}
