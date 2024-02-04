using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.BookFeatures.UpdateBook
{
    public class UpdateBookMapper : Profile
    {
        public UpdateBookMapper()
        {
            CreateMap<Book, UpdateBookRequest>().ReverseMap(); 
            CreateMap<Book, UpdateBookResponse>().ReverseMap();
        }
    }
}
