using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalRole : IEntity
    {
        public DalRole()
        {
            UsersIds = new HashSet<int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<int> UsersIds { get; set; }
    }
}