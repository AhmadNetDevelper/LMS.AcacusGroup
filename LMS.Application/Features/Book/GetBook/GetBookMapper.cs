using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.BookFeatures.GetBook
{
    public class GetBookMapper : Profile
    {
        public GetBookMapper()
        {
            CreateMap<BookResponse, Book>().ReverseMap();
        }
    }
}
