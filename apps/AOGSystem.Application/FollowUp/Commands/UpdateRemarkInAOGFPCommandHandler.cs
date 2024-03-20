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
    public class UpdateRemarkInAOGFPCommandHandler : IRequestHandler<UpdateRemarkInAOGFPCommand, ReturnDto<RemarkSummery>>
    {
        private readonly IAOGFollowUpRepository _followUpRepository;
        public UpdateRemarkInAOGFPCommandHandler(IAOGFollowUpRepository followUpRepository)
        {
            _followUpRepository = followUpRepository;
        }

        public async Task<ReturnDto<RemarkSummery>> Handle(UpdateRemarkInAOGFPCommand request, CancellationToken cancellationToken)
        {
            var model = await _followUpRepository.GetAOGFollowUpByIDAsync(request.AOGFollowUpId);
            if (model == null)
                return new ReturnDto<RemarkSummery>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 1,
                    Message = "AOG Follow-up cannot be found"
                };
            model.UpdateRemark(request.Id,
                request.AOGFollowUpId,
                request.Message);

            _followUpRepository.Update(model);
            var result = await _followUpRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<RemarkSummery>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 1,
                    Message = "Something went wrong when update remark"
                };

            var returnData = new RemarkSummery
            {
                Id = model.Id,
                AOGFollowUpId = request.AOGFollowUpId,
                Message = request.Message,
            };
            return new ReturnDto<RemarkSummery>
            {
                Data = returnData,
                IsSuccess = true,
                Count = 1,
                Message = "Remark Updated Successfully"
            };
        }
    }

    public class UpdateRemarkInAOGFPCommand : IRequest<ReturnDto<RemarkSummery>>
    {
        public Guid Id { get; set; }
        public Guid AOGFollowUpId { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }

        public UpdateRemarkInAOGFPCommand()
        {

        }
    }
}
