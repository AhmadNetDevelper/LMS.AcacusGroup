using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.Features.Auth.Query.IsUserExistByEmail
{
    public record IsUserExistByEmailRequest(string Email) : IRequest<bool>;
}
