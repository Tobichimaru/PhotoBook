﻿
using System.Collections.Generic;

namespace DAL.Interfacies.DTO
{
    public class DalProfile : IEntity
    {
        public DalProfile()
        {
            Photos = new HashSet<DalPhoto>();
            Likes = new HashSet<DalLike>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public byte[] Avatar { get; set; }

        public virtual ICollection<DalPhoto> Photos { get; set; }
        public virtual ICollection<DalLike> Likes { get; set; }
    }
}
