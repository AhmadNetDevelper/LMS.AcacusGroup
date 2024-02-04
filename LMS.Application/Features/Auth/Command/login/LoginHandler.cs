using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Auth.Command.login
{
    public class LoginHandler : IRequestHandler<LoginRequest, object>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public LoginHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            return await _authRepository.Login(request, cancellationToken);
        }
    }
}
