using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.Library.Query.GetBooksNotAdded
{
    public class LibraryBookMapper : Profile
    {
        public LibraryBookMapper()
        {
            CreateMap<Book, LibraryBookAddedResponse>().ReverseMap();
        }
    }
}
