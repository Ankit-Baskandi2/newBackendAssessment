using AssessementProjectForAddingUser.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class UserDetailsAnkitDtos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name can't be empty")]
        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name can't be empty")]
        public string? LastName { get; set; }

        public byte? Gender { get; set; }

        public DateOnly? DateOfjoining { get; set; }

        public DateOnly? Dob { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = null!;

        public string? AlternatePhone { get; set; }

        //public string? ImagePath { get; set; }

        public bool? IsActive { get; set; } = false;

        public virtual ICollection<UserAddressAnkit> UserAddressAnkits { get; set; } = new List<UserAddressAnkit>();
    }
}
