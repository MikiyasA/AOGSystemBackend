using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Users
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ReturnDto<User>>
    {
        private readonly UserManager<User> _userManager;
        public ActivateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ReturnDto<User>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if(user != null)
            {
                user.LockoutEnd = null;
                user.IsActive = true;
                user.UserStatus = "Activated";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ReturnDto<User>
                    {
                        Data = user,
                        IsSuccess = true,
                        Message = "User successfully activated",
                        Count = 1
                    };
                } else
                {
                    return new ReturnDto<User>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Problem occur when User activating",
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

    public class ActivateUserCommand : IRequest<ReturnDto<User>>
    {
        public string Id { get; set; }
    }
}
