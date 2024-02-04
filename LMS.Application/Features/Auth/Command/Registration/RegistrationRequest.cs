using MediatR;

namespace LMS.Application.Features.Auth.Command.Registration
{
    public record RegistrationRequest(int Id, string UserName, string NormalizedUserName,
                                      bool IsActive, string Email, bool EmailConfirmed,
                                      string PhoneNumber, bool PhoneNumberConfirmed,
                                      string PasswordHash, string NormalizedEmail,
                                      string SecurityStamp) : IRequest<bool>;
}
