using System;

namespace BLL.Interfacies.Entities
{
    public class ProfileEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int UserId { get; set; }
    }
}
