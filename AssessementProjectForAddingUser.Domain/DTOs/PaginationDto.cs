using System.ComponentModel;

namespace AssessementProjectForAddingUser.Domain.DTOs
{
    public class PaginationDto
    {
        [DefaultValue("")]
        public string? Name { get; set; }

        [DefaultValue("")]
        public string? ContactNo { get; set; }

        [DefaultValue(1)]
        public int PageNumber { get; set; }

        [DefaultValue(5)]
        public int PageSize { get; set; }
    }
}
