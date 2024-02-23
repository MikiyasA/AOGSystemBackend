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
    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, ReturnDto<PartQueryModel>>
    {
        private readonly IPartRepository _partRepository;
        public UpdatePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<ReturnDto<PartQueryModel>> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            var model = await _partRepository.GetPartByIDAsync(request.Id);
            model.SetPartNumber(request.PartNumber);
            model.SetDescription(request.Description);
            model.SetStockNo(request.StockNo);
            model.SetFinancialClass(request.FinancialClass);
            model.SetManufacturer(request.Manufacturer);
            model.SetPartType(request.PartType);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _partRepository.Update(model);

            var result = await _partRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<PartQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "Something wrong on Part update"
                };

            var returnData = new PartQueryModel
            {
                Id = model.Id,
                PartNumber = model.PartNumber,
                Description = model.Description,
                StockNo = model.StockNo,
                FinancialClass = model.FinancialClass,
                Manufacturer = model.Manufacturer,
                PartType = model.PartType
            };

            return new ReturnDto<PartQueryModel> { 
                Data = returnData,
                IsSuccess = true,
                Count = 1,
                Message = "Part Updated Successfully"
            };
        }
    }
    public class UpdatePartCommand : IRequest<ReturnDto<PartQueryModel>>
    {
        public Guid Id { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartType { get; set; }
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy)
        {
            UpdatedBy = updatedBy;
        }

        public UpdatePartCommand() { }

        public UpdatePartCommand(Guid id, string? partNumber, string? description, string? stockNo, string? financialClass)
        {
            Id = id;
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            FinancialClass = financialClass;
        }
    }
}
