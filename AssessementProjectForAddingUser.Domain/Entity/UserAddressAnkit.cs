
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessementProjectForAddingUser.Domain.Entity
{
    public class UserAddressAnkit
    {
        public long AddressId { get; set; }

        public string City { get; set; } = null!;

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? ZipCode { get; set; }

        public long? Userid { get; set; }

        public DateOnly? CreatedDate { get; set; }

        public DateOnly? UpdateDate { get; set; }

        public virtual UserDetailsAnkit? User { get; set; }
    }
}
