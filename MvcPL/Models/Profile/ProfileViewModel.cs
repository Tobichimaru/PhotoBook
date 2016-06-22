using System;
using System.Collections.Generic;
using DAL.Interfacies.DTO;

namespace MvcPL.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public byte[] Avatar { get; set; }
    }
}