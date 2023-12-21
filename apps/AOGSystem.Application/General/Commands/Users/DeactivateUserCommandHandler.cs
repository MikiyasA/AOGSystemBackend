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
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, ReturnDto<User>>
    {
        private readonly UserManager<User> _userManager;
        public DeactivateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        public async Task<ReturnDto<User>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user != null)
            {
                user.LockoutEnd = DateTime.Now.AddDays(-1);
                user.IsActive = false;
                user.UserStatus = "Deactivated";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ReturnDto<User>
                    {
                        Data = user,
                        IsSuccess = true,
                        Message = "User successfully deactivated",
                        Count = 1
                    };
                }
                else
                {
                    return new ReturnDto<User>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Problem occur when User deactivating",
                        Count = 0
                    };
                }

            }
            return new ReturnDto<User>
            {
                Data = null,
                IsSuccess = false,
                Message = "User can not be found",
                Count = 0
            };
        }
    
    }

    public class DeactivateUserCommand : IRequest<ReturnDto<User>>
    {
        public string Id { get; set; }
    }
}
