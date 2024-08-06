
namespace AssessementProjectForAddingUser.Domain.Entity
{
    public class UserDetailsAnkit
    {
        public long UserId { get; set; }

        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = null!;

        public byte? Gender { get; set; }

        public DateOnly? DateOfjoining { get; set; }

        public DateOnly? Dob { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? AlternatePhone { get; set; }

        public string? ImagePath { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? UpdateDate { get; set; }

        public virtual ICollection<UserAddressAnkit> UserAddressAnkits { get; set; } = new List<UserAddressAnkit>();
    }
}
