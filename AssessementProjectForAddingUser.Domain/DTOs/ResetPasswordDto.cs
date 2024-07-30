using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        public string Password { get; set; }
    }
}
