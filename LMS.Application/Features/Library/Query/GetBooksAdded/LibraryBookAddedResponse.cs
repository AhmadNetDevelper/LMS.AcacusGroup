namespace LMS.Application.Features.Library.Query.GetBooksNotAdded
{
    public class LibraryBookAddedResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public bool IsAvailable { get; set; }
    }
}
