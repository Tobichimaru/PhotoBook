using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Services
{
    public interface IService<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAllEntities();
        void Create(T item);
        void Delete(T item);
    }
}
