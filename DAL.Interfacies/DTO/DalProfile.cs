using System;

namespace DAL.Interfacies.DTO
{
    class DalProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public int UserId { get; set; }
    }
}
