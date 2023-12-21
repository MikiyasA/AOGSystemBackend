using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class UpdateFPTabCommandHandler : IRequestHandler<UpdateFPTabCommand, ReturnDto<FollowUpTabsSummery>>
    {
        private readonly IFollowUpTabsRepository _followUpTabsRepository;
        public UpdateFPTabCommandHandler(IFollowUpTabsRepository followUpTabsRepository)
        {
            _followUpTabsRepository= followUpTabsRepository;
        }
        public async Task<ReturnDto<FollowUpTabsSummery>> Handle(UpdateFPTabCommand request, CancellationToken cancellationToken)
        {
            var model = await _followUpTabsRepository.GetFollowUpTabsByIDAsync(request.Id);
            if(model == null)
                return new ReturnDto<FollowUpTabsSummery>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Tab cannot be found. Please check if you are updating existed tab"

                };
            model.SetName(request.Name);
            model.SetColor(request.Color);
            model.SetStatus(request.Status);
            model.UpdatedAT= DateTime.Now;

            var result = await _followUpTabsRepository.SaveChangesAsync();

            if(request == null)
            {
                return new ReturnDto<FollowUpTabsSummery>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something is wrong when updating follow up tab"

                };
            }

            var followUpTab = new FollowUpTabsSummery
            {
                Name = request.Name,
                Color = model.Color,
                Status = model.Status,
            };

            return new ReturnDto<FollowUpTabsSummery>
            {
                Data = followUpTab,
                Count = 1,
                IsSuccess = true,
                Message = "Follow Up Tab updated successfully"
            };
        }
    }

    public class UpdateFPTabCommand : IRequest<ReturnDto<FollowUpTabsSummery>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
    }
}
