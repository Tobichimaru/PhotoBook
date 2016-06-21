using System;
using System.ComponentModel.DataAnnotations;

namespace MvcPL.Models
{
    public class ProfileEditModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Enter your name")]
        [StringLengthAttribute(50, ErrorMessage = "The name must contain at lest {2} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Enter your surname")]
        [StringLength(100, ErrorMessage = "The surname must contain at least {2} characters", MinimumLength = 4)]
        public string LastName { get; set; }

        [Display(Name = "Enter your age")]
        public int Age { get; set; }

        [Display(Name = "Upload new avatar")]
        public byte[] Avatar { get; set; }
        
    }
}