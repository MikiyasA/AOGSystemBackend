using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Part
{
    public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, PartQueryModel>
    {
        private readonly IPartRepository _partRepository;
        public CreatePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }
        public async Task<PartQueryModel> Handle(CreatePartCommand request, CancellationToken cancellationToken)
        {
            var part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if (part != null)
                return null;

            var model = new Domain.General.Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass);
            model.CreatedAT = DateTime.UtcNow;
            _partRepository.Add(model);
            var result = await _partRepository.SaveChangesAsync();
            if (result == 0)
                return null;
            return new PartQueryModel
            {
                Id = model.Id,
                PartNumber = model.PartNumber,
                Description = model.Description,
                StockNo = model.StockNo,
                FinancialClass = model.FinancialClass,
            };

        }
    }
    public class CreatePartCommand  : IRequest<PartQueryModel>
    {
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }

        public CreatePartCommand() { }
        public CreatePartCommand(string partNumber, string description, string stockNo, string financialClass)
        {
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            FinancialClass = financialClass;
        }
    }
}
