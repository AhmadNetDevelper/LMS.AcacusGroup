using AutoMapper;
using LMS.Domain.Entities;

namespace LMS.Application.Features.Auth.Command.login
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
            CreateMap<LoginResponse, User>().ReverseMap();
        }
    }
}
