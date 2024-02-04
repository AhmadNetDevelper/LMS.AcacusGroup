using AutoMapper;
using LMS.Application.Features.Auth.Command.login;
using LMS.Application.Features.Auth.Command.Registration;
using LMS.Application.Features.Auth.Query.IsUserExistByEmail;
using LMS.Application.Repositories;
using LMS.Domain.Entities;
using LMS.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMS.Persistence.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        protected readonly DataContext Context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepository(DataContext context, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, IMapper mapper)
        {
            Context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost("UserLogin")]
        public async Task<object> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var userManager = await FindUserByEmail(request.Email);

            var result = _signInManager.CheckPasswordSignInAsync(userManager, request.Password, false);

            if (!result.IsCompletedSuccessfully)
            {
                return null;
            }

            var appUser = _userManager.Users.FirstOrDefault(u => u.Email == request.Email);
            var userToReturn = _mapper.Map<LoginResponse>(appUser);

            return new
            {
                token = GenerateJwtTokenAsync(appUser).Result,
                user = userToReturn
            };
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                var tt = _configuration.GetSection("AppSettings:Token").Value;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var tokenDedcription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDedcription);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Registration(RegistrationRequest request, CancellationToken cancellationToken)
        {
            RegistrationRequest registrationRequest = new
                RegistrationRequest(request.Id, request.UserName, request.NormalizedUserName, true, request.Email,
                true, request.PhoneNumber, true, request.PasswordHash, request.Email.ToUpper(), Guid.NewGuid().ToString());

            Role role = Context.Roles.Where(x => x.Id == 3).FirstOrDefault();
            User userToCreate = _mapper.Map<User>(registrationRequest);
            IdentityResult result = _userManager.CreateAsync(userToCreate, registrationRequest.PasswordHash).Result;

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToCreate, role.Name);


                //add Patron based on user
                Context.Patrons.Add(new Patron
                {
                    FirstName = request.UserName,
                    LastName = request.UserName,
                    UserId = userToCreate.Id,
                });


                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> IsUserExistByEmail(IsUserExistByEmailRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user is not null)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
