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
    public class UnassignsUserFromCommandHandler : IRequestHandler<UnassignsUserFromCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public UnassignsUserFromCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(UnassignsUserFromCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null)
                return IdentityResult.Failed(new IdentityError { Description = "There is no user with this username." });
            var role = await _roleManager.FindByNameAsync(request.Role);
            if(role == null)
                return IdentityResult.Failed(new IdentityError { Description = "The role can not be found." });

            if (await _userManager.IsInRoleAsync(user, request.Role))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, request.Role);
                return result;
            } else
            {
                return IdentityResult.Failed(new IdentityError { Description = "The user is not assigned on this role." });
            }
        }
    }
    
    public class UnassignsUserFromCommand : IRequest<IdentityResult>
    {
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
