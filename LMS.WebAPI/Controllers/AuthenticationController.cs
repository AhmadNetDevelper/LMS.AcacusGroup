using LMS.Application.Features.Auth.Command.login;
using LMS.Application.Features.Auth.Command.Registration;
using LMS.Application.Features.Auth.Query.IsUserExistByEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.WebAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("user-login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Register(RegistrationRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpGet("is-user-exist-by-email")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> IsUserExistsByEmail(string userEmail, CancellationToken cancellationToken)
        {
            IsUserExistByEmailRequest emailRequest = new IsUserExistByEmailRequest(userEmail);

            var response = await _mediator.Send(emailRequest, cancellationToken);
            return Ok(response);
        }

    }
}
