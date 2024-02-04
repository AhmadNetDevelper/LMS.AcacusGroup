using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Auth.Command.Registration
{
    public class RegistrationHandler : IRequestHandler<RegistrationRequest, bool>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public RegistrationHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            return await _authRepository.Registration(request, cancellationToken);
        }
    }
}
