using AutoMapper;
using LMS.Application.Repositories;
using MediatR;

namespace LMS.Application.Features.Auth.Query.IsUserExistByEmail
{
    public class IsUserExistByEmailHandler : IRequestHandler<IsUserExistByEmailRequest, bool>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public IsUserExistByEmailHandler(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(IsUserExistByEmailRequest request, CancellationToken cancellationToken)
        {
            return await _authRepository.IsUserExistByEmail(request, cancellationToken);
        }
    }
}
