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
    public class AddRemarkInAOGFPCommandHandler : IRequestHandler<AddRemarkInAOGFPCommand, RemarkSummery>
    {
        private readonly IAOGFollowUpRepository _followUpRepository;
        private readonly IMediator _mediator;
        public AddRemarkInAOGFPCommandHandler(IAOGFollowUpRepository followUpRepository, IMediator mediator)
        {
            _followUpRepository = followUpRepository;
            _mediator = mediator;
        }

        public async Task<RemarkSummery> Handle(AddRemarkInAOGFPCommand request, CancellationToken cancellationToken)
        {

            var model = await _followUpRepository.GetAOGFollowUpByIDAsync(request.AOGFollowUpId);
            var newRemark = new Remark(request.AOGFollowUpId, request.Message);
            newRemark.CreatedAT= DateTime.UtcNow;
            model.AddRemark(newRemark);
            _followUpRepository.Update(model);

            var result = await _followUpRepository.SaveChangesAsync();
            if (result == 0)
                return null;

            return new RemarkSummery
            {
                Id = newRemark.Id,
                AOGFollowUpId = request.AOGFollowUpId,
                Message = request.Message,
            };
        }
    }

    public class AddRemarkInAOGFPCommand : IRequest<RemarkSummery>
    {
        public int AOGFollowUpId { get; set; }
        public string? Message { get; set; }

        public AddRemarkInAOGFPCommand() { }
    }
}
