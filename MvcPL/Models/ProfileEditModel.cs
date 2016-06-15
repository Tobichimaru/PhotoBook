using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class ProfileEditModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your name")]
        [Required(ErrorMessage = "The field can not be empty!")]
        public string FirstName { get; set; }

        [Display(Name = "Enter your surname")]
        public string LastName { get; set; }

        [Display(Name = "Enter your age")]
        public int Age { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdateDate { get; set; }
        
    }
}