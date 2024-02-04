using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.BookFeatures.CreateBook
{
    public class CreateBookMapper : Profile
    {
        public CreateBookMapper()
        {
            CreateMap<BookResponse, Book>().ReverseMap();
            CreateMap<CreateBookRequest, Book>().ReverseMap();
            CreateMap<CreateBookResponse, Book>().ReverseMap();
        }
    }
}
