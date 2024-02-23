using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class DeleteAOGFPCommandHandler : IRequestHandler<DeleteAOGFPCommand, int>
    {
        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        private readonly IMediator _mediator;
        public DeleteAOGFPCommandHandler(IAOGFollowUpRepository AOGFollowUpRepository, IMediator mediator)
        {
            _AOGFollowUpRepository = AOGFollowUpRepository;
            _mediator = mediator;
        }

        public async Task<int> Handle(DeleteAOGFPCommand request, CancellationToken cancellationToken)
        {
            var data = await _AOGFollowUpRepository.GetAOGFollowUpByIDAsync(request.Id);
            if (data != null)
            {
                _AOGFollowUpRepository.Delete(request.Id);
                return await _AOGFollowUpRepository.SaveChangesAsync();
            }
            else
            {
                // TODO
                throw new Exception($"Entity with Id {request.Id} not found.");
            }
        }
    }
    public class DeleteAOGFPCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

}
