using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Features.BookFeatures.CreateBook
{
    public class CreateBookResponse
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public bool IsAvailable { get; set; }
    }
}
