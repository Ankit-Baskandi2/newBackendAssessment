using AssessementProjectForAddingUser.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual UserDetailsAnkit? User { get; set; }
    }
}
