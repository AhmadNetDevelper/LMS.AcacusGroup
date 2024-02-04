using AutoMapper;
using LMS.Application.Features.Library.Query.GetBooksNotAdded;
using LMS.Domain.Entities;

namespace LMS.Application.Features.Library.Query.GetBookNotAdded
{
    public class NotAddedBookMapper : Profile
    {
        public NotAddedBookMapper()
        {
            CreateMap<Book, LibraryBookAddedRequest>().ReverseMap();
            CreateMap<Book, LibraryBookAddedResponse>().ReverseMap();
        }
    }
}
