using LMS.Domain.Common;

namespace LMS.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public bool IsAvailable { get; set; }

        public long? LibraryId { get; set; }
        public virtual Library? Library { get; set; }
    }
}
