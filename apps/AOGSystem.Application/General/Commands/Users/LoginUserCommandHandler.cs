using AOGSystem.Domain.General;
using MassTransit.JobService;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static AOGSystem.Application.General.Commands.Users.LoginUserCommandHandler;

namespace AOGSystem.Application.General.Commands.Users
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService; // Assuming you have a JWT service

        public LoginUserCommandHandler(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(!user.IsActive)
                return new LoginResponse
                {
                    Error = "The user must be activated before attempting login"
                };

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Name, user.UserName),
                };

                var roles = await _userManager.GetRolesAsync(user);
                var roleNames = roles.ToList();

                foreach (var roleName in roleNames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }

                var token = _jwtService.GenerateToken(user.Id.ToString(), claims);

                return new LoginResponse
                {
                    IdentityResult = IdentityResult.Success, // Or your custom result
                    Token = token
                };
            }

            return new LoginResponse
            {
                IdentityResult = IdentityResult.Failed(new IdentityError { Description = "Invalid username or password." }),
                Token = null
            };

        }

        public class LoginResponse
        {
            public IdentityResult IdentityResult { get; set; }
            public string Token { get; set; }
            public string? Error { get; set; }
        }


        public class LoginUserCommand : IRequest<LoginResponse>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
