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
    public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, ReturnDto<PartQueryModel>>
    {
        private readonly IPartRepository _partRepository;
        public CreatePartCommandHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }
        public async Task<ReturnDto<PartQueryModel>> Handle(CreatePartCommand request, CancellationToken cancellationToken)
        {
            var part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if (part != null)
                return new ReturnDto<PartQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "This Part Number already existed"
                }; ;

            var model = new Domain.General.Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass, request.Manufacturer, request.PartType);
            model.CreatedAT = DateTime.Now;
            _partRepository.Add(model);
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

            return new ReturnDto<PartQueryModel>
            {
                Data = returnData,
                IsSuccess = true,
                Count = 1,
                Message = "Part Updated Successfully"
            };

        }
    }
    public class CreatePartCommand  : IRequest<ReturnDto<PartQueryModel>>
    {
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartType { get; set; }

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
