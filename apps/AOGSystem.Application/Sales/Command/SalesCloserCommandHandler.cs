using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Sales.Query;
using AOGSystem.Domain.Sales;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Sales.Command
{
    public class SalesCloserCommandHandler : IRequestHandler<SalesCloserCommand, ReturnDto<SalesQueryModel>>
    {
        private readonly ISaleRepository _saleRepository;
        public SalesCloserCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
    
        public async Task<ReturnDto<SalesQueryModel>> Handle(SalesCloserCommand request, CancellationToken cancellationToken)
        {
            var model = await _saleRepository.GetSalesByIDAsync(request.Id);
            if (model == null)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Sales order can not be found"
                };
            model.SetStatus(request.Status);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            var result = await _saleRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when shipmnet for sales order processed",
                };
            var returnDate = new SalesQueryModel
            {
                Id = model.Id,
                CompanyId = model.CompanyId,
                OrderByName = model.OrderByName,
                OrderByEmail = model.OrderByEmail,
                OrderNo = model.OrderNo,
                CustomerOrderNo = model.CustomerOrderNo,
                ShipToAddress = model.ShipToAddress,
                Status = model.Status,
                IsApproved = model.IsApproved,
                Note = model.Note,
                IsFullyShipped = model.IsFullyShipped,
                AWBNo = model.AWBNo,
                ShipDate = model.ShipDate,
                ReceivedByCustomer = model.ReceivedByCustomer,
                ReceivedDate = model.ReceivedDate,
            };
            var message = model.Status == "Closed" ? "Loan order closed successfully" : model.Status == "Re-Opened" ? "Loan order re-opened successfully" : "";
            return new ReturnDto<SalesQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = message
            };
        }
    }

    public class SalesCloserCommand : IRequest<ReturnDto<SalesQueryModel>>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
