using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Users
{
    public class CreateNewRoleCommandHandler : IRequestHandler<CreateNewRoleCommand, IdentityResult>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public CreateNewRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> Handle(CreateNewRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByNameAsync(request.Role);
            if (role != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "This role is already created." });
            }
            var newRole = new IdentityRole<Guid> { Name = request.Role };
            var result = await _roleManager.CreateAsync(newRole);
            return result;
        }
    }

    public class CreateNewRoleCommand : IRequest<IdentityResult>
    {
        public string Role { get; set; }
    }

}
