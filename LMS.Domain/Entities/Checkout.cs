using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Checkout : BaseEntity
    {
        public DateTimeOffset CheckoutDate { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool IsReturned { get; set; }
        public bool IsApproved { get; set; }
        public long BookId { get; set; }
        public virtual Book Book { get; set; }
        public long PatronId { get; set; }
        public virtual Patron Patron { get; set; }
    }
}
