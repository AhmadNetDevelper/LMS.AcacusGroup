using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Library : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
