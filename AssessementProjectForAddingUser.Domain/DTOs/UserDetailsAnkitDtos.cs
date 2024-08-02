using AssessementProjectForAddingUser.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [Required(ErrorMessage ="Date of birth can't be empty")]
        public DateOnly? Dob { get; set; }

        [Required(ErrorMessage = "Email can't be empty")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = null!;

        public string? AlternatePhone { get; set; }

        public IFormFile ImagePath { get; set; }

        public bool? IsActive { get; set; } = false;

        public virtual ICollection<UserAddressAnkit> UserAddressAnkits { get; set; } = new List<UserAddressAnkit>();
    }
}
