using AOGSystem.Domain.CoreFollowUps;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AOGSystem.Application.CoreFollowUps.Commands
{
    public class DeleteCoreFollowUpCommandHandler : IRequestHandler<DeleteCoreFollowUpCommand, int>
    {
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        public DeleteCoreFollowUpCommandHandler(ICoreFollowUpRepository coreFollowUpRepository)
        {
            _coreFollowUpRepository = coreFollowUpRepository;
        }

public async Task<int> Handle(DeleteCoreFollowUpCommand request, CancellationToken cancellationToken)
        {
            var data = await _coreFollowUpRepository.GetCoreFollowUpByIDAsync(request.Id);
            if(data != null)
            {
                _coreFollowUpRepository.Delete(request.Id);
                return await _coreFollowUpRepository.SaveChangesAsync();
            } // TODO - return message 
            return 0;
        }
    }
    public class DeleteCoreFollowUpCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
