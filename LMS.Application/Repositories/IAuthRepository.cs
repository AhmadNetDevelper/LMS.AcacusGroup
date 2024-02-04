using LMS.Application.Features.Auth.Command.login;
using LMS.Application.Features.Auth.Command.Registration;
using LMS.Application.Features.Auth.Query.IsUserExistByEmail;

namespace LMS.Application.Repositories
{
    public interface IAuthRepository
    {
        Task<object> Login(LoginRequest request, CancellationToken cancellationToken);
        Task<bool> Registration(RegistrationRequest request, CancellationToken cancellationToken);
        Task<bool> IsUserExistByEmail(IsUserExistByEmailRequest request, CancellationToken cancellationToken);
    }
}
