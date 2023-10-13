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
    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, PartQueryModel>
    {
        private readonly IPartRepository _partRepository;
        public UpdatePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<PartQueryModel> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var model = await _partRepository.GetPartByIDAsync(request.Id);
            model.SetPartNumber(request.PartNumber);
            model.SetDescription(request.Description);
            model.SetStockNo(request.StockNo);
            model.SetFinancialClass(request.FinancialClass);
            model.UpdatedAT = DateTime.UtcNow;

            _partRepository.Update(model);

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
    public class UpdatePartCommand : IRequest<PartQueryModel>
    {
        public int Id { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }

        public UpdatePartCommand() { }

        public UpdatePartCommand(int id, string? partNumber, string? description, string? stockNo, string? financialClass)
        {
            Id = id;
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            FinancialClass = financialClass;
        }
    }
}
