using AOGSystem.Domain.Quotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Commands
{
    public class DeleteQuotationCommandHandler : IRequestHandler<DeleteQuotationCommand, int>
    {
        private readonly IQuotationRepository _quotationRepository;
        public DeleteQuotationCommandHandler(IQuotationRepository quotationRepository)
        {
            _quotationRepository = quotationRepository;
        }

        public async Task<int> Handle(DeleteQuotationCommand request, CancellationToken cancellationToken)
        {
            var data = await _quotationRepository.GetQuotationByIdAsync(request.Id);
            if (data != null)
            {
                _quotationRepository.Delete(request.Id);
                return await _quotationRepository.SaveChangesAsync();
            }
            else
            {
                // TODO
                throw new Exception($"Entity with Id {request.Id} not found.");
            }
        }
    }
    public class DeleteQuotationCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}
