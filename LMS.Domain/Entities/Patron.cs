using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Patron : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTimeOffset? DateOfBirth { get; set; }
        public string DepartmentName { get; set; }
        public long UserId { get; set; }
        public virtual ICollection<Checkout> Checkouts { get; set; } = null!;
    }
}
