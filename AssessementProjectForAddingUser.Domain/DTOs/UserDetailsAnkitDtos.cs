using AssessementProjectForAddingUser.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class UserDetailsAnkitDtos
    {
        //[Required(ErrorMessage = "First name can't be empty")]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        //[Required(ErrorMessage = "Last name can't be empty")]
        public string LastName { get; set; }

        public byte? Gender { get; set; }

        public DateOnly? DateOfjoining { get; set; }

        public DateOnly? Dob { get; set; }

        //[Required(ErrorMessage = "Email can't be empty")]
        //[EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; } = null!;

        //[Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = null!;

        public string? AlternatePhone { get; set; }

        //public string? ImagePath { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<UserAddressAnkit> UserAddressAnkits { get; set; } = new List<UserAddressAnkit>();
    }
}
