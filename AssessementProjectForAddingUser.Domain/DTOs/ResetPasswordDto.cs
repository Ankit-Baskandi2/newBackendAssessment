using System.ComponentModel.DataAnnotations;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; }
    }
}
