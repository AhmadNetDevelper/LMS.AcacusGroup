
using MediatR;

namespace LMS.Application.Features.Auth.Command.login
{
    public record LoginRequest(string Email, string Password) : IRequest<object>;
}
