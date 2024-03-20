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
    public class AddRemarkInAOGFPCommandHandler : IRequestHandler<AddRemarkInAOGFPCommand, ReturnDto<RemarkSummery>>
    {
        private readonly IAOGFollowUpRepository _followUpRepository;
        private readonly IMediator _mediator;
        public AddRemarkInAOGFPCommandHandler(IAOGFollowUpRepository followUpRepository, IMediator mediator)
        {
            _followUpRepository = followUpRepository;
            _mediator = mediator;
        }

        public async Task<ReturnDto<RemarkSummery>> Handle(AddRemarkInAOGFPCommand request, CancellationToken cancellationToken)
        {

            var model = await _followUpRepository.GetAOGFollowUpByIDAsync(request.AOGFollowUpId);
            if(model == null)
                return new ReturnDto<RemarkSummery>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 1,
                    Message = "AOG Follow-up cannot be found"
                };
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;
            var newRemark = new Remark(request.AOGFollowUpId, request.Message);
            newRemark.CreatedAT= DateTime.Now;
            newRemark.CreatedBy = request.CreatedBy;
            model.AddRemark(newRemark);
            _followUpRepository.Update(model);

            var result = await _followUpRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<RemarkSummery>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 1,
                    Message = "Something went wrong when add remark"
                };

            var returnData = new RemarkSummery
            {
                Id = newRemark.Id,
                AOGFollowUpId = request.AOGFollowUpId,
                Message = request.Message,
            };
            return new ReturnDto<RemarkSummery>
            {
                Data = returnData,
                IsSuccess = true,
                Count = 1,
                Message = "Remark added successfully"
            };
        }
    }

    public class AddRemarkInAOGFPCommand : IRequest<ReturnDto<RemarkSummery>>
    {
        public Guid AOGFollowUpId { get; set; }
        public string? Message { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }

        public AddRemarkInAOGFPCommand() { }
    }
}
