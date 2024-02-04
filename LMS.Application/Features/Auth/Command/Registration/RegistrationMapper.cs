using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.Auth.Command.Registration
{
    public class RegistrationMapper : Profile
    {
        public RegistrationMapper() 
        {
            CreateMap<RegistrationRequest, User>().ReverseMap();
        }
    }
}
