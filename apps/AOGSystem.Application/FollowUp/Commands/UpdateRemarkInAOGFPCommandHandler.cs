using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class UpdateRemarkInAOGFPCommandHandler : IRequestHandler<UpdateRemarkInAOGFPCommand, RemarkSummery>
    {
        private readonly IAOGFollowUpRepository _followUpRepository;
        public UpdateRemarkInAOGFPCommandHandler(IAOGFollowUpRepository followUpRepository)
        {
            _followUpRepository = followUpRepository;
        }

        public async Task<RemarkSummery> Handle(UpdateRemarkInAOGFPCommand request, CancellationToken cancellationToken)
        {
            var model = await _followUpRepository.GetAOGFollowUpByIDAsync(request.AOGFollowUpId);
            model.UpdateRemark(request.Id,
                request.AOGFollowUpId,
                request.Message);


            _followUpRepository.Update(model);
            var result = await _followUpRepository.SaveChangesAsync();
            if (result == 0)
                return null;

            return new RemarkSummery
            {
                Id = request.Id,
                AOGFollowUpId = request.AOGFollowUpId,
                Message = request.Message,
            };
        }
    }

    public class UpdateRemarkInAOGFPCommand : IRequest<RemarkSummery>
    {
        public int Id { get; set; }
        public int AOGFollowUpId { get; set; }
        public string Message { get; set; }
        public UpdateRemarkInAOGFPCommand()
        {

        }
    }
}
