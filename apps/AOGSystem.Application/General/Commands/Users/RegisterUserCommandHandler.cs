using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Users
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.FirstName, request.LastName, request.UserName, request.PhoneNumber, request.Email);
            user.UpdatedAT= DateTime.UtcNow;
            if(request.Password != request.ConfirmPassword)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Password and ConfirmPassword do not match." });
            }
            var result = await _userManager.CreateAsync(user, request.Password);

            return result;

        }
    }
    public class RegisterUserCommand : IRequest<IdentityResult> 
    {
        public string UserName { get; set;}
        public string Password { get; set;}
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
