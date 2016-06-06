using System.Collections.Generic;

namespace BLL.Interfacies.Entities
{
    public class RoleEntity
    {
        public RoleEntity()
        {
            UsersIds = new HashSet<int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<int> UsersIds { get; set; }
    }
}