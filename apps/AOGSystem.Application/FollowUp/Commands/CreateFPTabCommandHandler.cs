using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class CreateFPTabCommandHandler : IRequestHandler<CreateFPTabCommand, ReturnDto<FollowUpTabsSummery>>
    {
        private readonly IFollowUpTabsRepository _followUpTabsRepository;
        public CreateFPTabCommandHandler(IFollowUpTabsRepository followUpTabsRepository)
        {
            _followUpTabsRepository = followUpTabsRepository;
        }

        public async Task<ReturnDto<FollowUpTabsSummery>> Handle(CreateFPTabCommand request, CancellationToken cancellationToken)
        {
            var fpExist = await _followUpTabsRepository.GetFollowUpTabsByNameAsync(request.Name);
            if (fpExist != null)
                return new ReturnDto<FollowUpTabsSummery>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Tab with this name is already exist. Please use existed tab or update tab name"
                };

            var model = new FollowUpTabs(request.Name, request.Color, request.Status);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _followUpTabsRepository.Add(model);
            var followUpTabsSummery = new FollowUpTabsSummery { 
                Name = request.Name,
                Color= model.Color,
                Status= model.Status,
            };
            var result = await _followUpTabsRepository.SaveChangesAsync();

            if (result == 0)
                return new ReturnDto<FollowUpTabsSummery>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something is wrong when creating follow up tab"
                };

            return new ReturnDto<FollowUpTabsSummery>
            {
                Data = followUpTabsSummery,
                Count = result != null ? 1 : 0,
                IsSuccess = true,
                Message = "Follow Up Tab created successfully"
            };
        }
    }

    public class CreateFPTabCommand : IRequest<ReturnDto<FollowUpTabsSummery>>
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
