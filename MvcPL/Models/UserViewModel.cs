using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{

    public class UserViewModel
    {
        [Display(Name = "User's e-mail")]
        public string Email { get; set; }

        [Display(Name = "Date of user's registration")]
        public DateTime CreationDate { get; set; }

        public int RoleId { get; set; }

        public string ImageToShow { get; set; }
    }
}