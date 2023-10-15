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
    public class AssignUserToRoleCommandHandler : IRequestHandler<AssignUserToRoleCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        public AssignUserToRoleCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityResult> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with this username." });
            }
            var result = await _userManager.AddToRoleAsync(user, request.Role);

            return result;

        }
    }
    public class AssignUserToRoleCommand : IRequest<IdentityResult> 
    {
        public string UserName { get; set;}
        //public string Email { get; set; }
        public string Role { get; set; }
    }
}
