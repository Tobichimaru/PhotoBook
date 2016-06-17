using System;

namespace MvcPL.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public byte[] Avatar { get; set; }
    }
}