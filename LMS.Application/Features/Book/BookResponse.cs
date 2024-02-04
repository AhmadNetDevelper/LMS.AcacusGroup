namespace LMS.Application.Features.BookFeatures
{
    public class BookResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; }
    }
}
