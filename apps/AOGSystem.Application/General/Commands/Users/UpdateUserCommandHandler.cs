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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ReturnDto<User>>
    {
        private readonly UserManager<User> _userManager;
        public UpdateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ReturnDto<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if(user == null)
                return new ReturnDto<User>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "This user can not be found User",
                    Count = 0
                };
            user.FirstName= request.FirstName;
            user.LastName= request.LastName;
            user.Email= request.Email;
            user.PhoneNumber= request.PhoneNumber;
            user.UpdatedAT = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ReturnDto<User>
                {
                    Data = user,
                    IsSuccess = true,
                    Message = "User successfully updated",
                    Count = 1
                };
            }
            else
            {
                return new ReturnDto<User>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Problem occur when update User",
                    Count = 0
                };
            }
        }
    }

    public class UpdateUserCommand : IRequest<ReturnDto<User>>
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
