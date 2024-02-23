using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Part
{
    public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, int>
    {
        private  readonly IPartRepository _partRepository;
        public DeletePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<int> Handle(DeletePartCommand request, CancellationToken cancellationToken)
        {
            var data = await _partRepository.GetPartByIDAsync(request.Id);
            if (data != null)
            {
                _partRepository.Delete(request.Id);
                return await _partRepository.SaveChangesAsync();
            }
            else
            {
                // TODO
                throw new Exception($"Entity with Id {request.Id} not found.");
            }
        }
    }
    public class DeletePartCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }
}
