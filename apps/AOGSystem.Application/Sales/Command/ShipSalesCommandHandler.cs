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
    public class ShipSalesCommandHandler : IRequestHandler<ShipSalesCommand, ReturnDto<SalesQueryModel>>
    {
        private readonly ISaleRepository _saleRepository;
        public ShipSalesCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<ReturnDto<SalesQueryModel>> Handle(ShipSalesCommand request, CancellationToken cancellationToken)
        {
            var model = await _saleRepository.GetSalesByIDAsync(request.SalesId);
            if (model == null)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Sales order can not be found"
                };
            model.SetIsFullyShipped(request.IsFullyShipped);
            model.SetAWBNO(request.AWBNo);
            model.SetShipDate(request.ShipDate);
            model.SetRecievedByCustomer(request.ReceivedByCustomer);
            model.SetReceivedDate(request.ReceivedDate);
            model.UpdatedAT = DateTime.Now;

            var status = request.ReceivedByCustomer ? "Received by Customer" : "Part Sent";
            model.SetStatus(status);

            model.UpdatedBy = request.UpdatedBy;

            _saleRepository.Update(model);
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
                Note = model.Note,
                IsFullyShipped = model.IsFullyShipped,
                AWBNo = model.AWBNo,
                ShipDate = model.ShipDate,
                ReceivedByCustomer = model.ReceivedByCustomer,
                ReceivedDate = model.ReceivedDate,
            };
            return new ReturnDto<SalesQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = "Sales order parts ship successfully"
            };
        }
    }
    public class ShipSalesCommand : IRequest<ReturnDto<SalesQueryModel>>
    {
        public int SalesId { get; set; }
        public bool IsFullyShipped { get;  set; } 
        public string? AWBNo { get;  set; }
        public DateTime? ShipDate { get;  set; }
        public bool ReceivedByCustomer { get;  set; } 
        public DateTime? ReceivedDate { get;  set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
