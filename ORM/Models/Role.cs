using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM.Models
{
    public class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}