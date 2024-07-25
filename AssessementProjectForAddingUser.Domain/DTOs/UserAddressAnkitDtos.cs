
namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class UserAddressAnkitDtos
    {
        public long AddressId { get; set; }

        public string City { get; set; } = null!;

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ZipCode { get; set; }

        public long? Userid { get; set; }
    }
}
