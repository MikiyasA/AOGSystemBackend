using AOGSystem.Application.FollowUp.HomeBase.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.HomeBase.Commands
{
    public class AddRemarkInHomeBaseFPCommandHandler : IRequestHandler<AddRemarkInHomeBaseFPCommand, RemarkSummery>
    {
        private readonly IRemarkRepository _remarkRepository;
        public AddRemarkInHomeBaseFPCommandHandler(IRemarkRepository remarkRepository)
        {
            _remarkRepository = remarkRepository;
        }

        public async Task<RemarkSummery> Handle(AddRemarkInHomeBaseFPCommand request, CancellationToken cancellationToken)
        {

            var model = new Remark(request.HomeBaseFollowUpId, request.Message);
            model.CreatedAT = DateTime.Now;
            _remarkRepository.Add(model);

            return new RemarkSummery
            {
                Id = model.Id,
                HomeBaseFollowUpId = model.HomeBaseFollowUpId,
                Message = model.Message,
            };
        }
    }

    public class AddRemarkInHomeBaseFPCommand : IRequest<RemarkSummery> 
    {
        public int HomeBaseFollowUpId { get; set; }
        public string? Message { get; private set; }

        public AddRemarkInHomeBaseFPCommand() { }
    }
}
