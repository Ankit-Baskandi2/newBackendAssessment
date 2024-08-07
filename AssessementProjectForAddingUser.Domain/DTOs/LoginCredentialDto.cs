using System.ComponentModel.DataAnnotations;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class LoginCredentialDto
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string? Password { get; set; }
    }
}
