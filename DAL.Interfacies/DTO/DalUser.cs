using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalUser : IEntity
    {
        public DalUser()
        {
            Photos = new HashSet<DalPhoto>();
            Likes = new HashSet<DalLike>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; } //MD5 hash
        public string Email { get; set; }
        public int ProfileId { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<DalPhoto> Photos { get; set; }
        public virtual ICollection<DalLike> Likes { get; set; }
    }
}