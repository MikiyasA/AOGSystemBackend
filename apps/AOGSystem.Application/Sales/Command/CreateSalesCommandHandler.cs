using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Sales.Query;
using AOGSystem.Domain.General;
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
    public class CreateSalesCommandHandler : IRequestHandler<CreateSalesCommand, ReturnDto<SalesQueryModel>>
    {
        private readonly ISaleRepository _saleRepository;
        public CreateSalesCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<ReturnDto<SalesQueryModel>> Handle(CreateSalesCommand request, CancellationToken cancellationToken)
        {
            var lastOrder = await _saleRepository.GetLastSalesOrder();
            int currentYear = DateTime.Now.Year;
            var nextOrderNo = lastOrder  == null ? 1 : OrderUtility.GetNextOrderNo(lastOrder.OrderNo);

            var orderNo = $"S{currentYear}{nextOrderNo:D2}";

            var model = new Domain.Sales.Sales(request.CompanyId, request.OrderByName, request.OrderByEmail, orderNo, request.CustomerOrderNo, request.ShipToAddress, request.Status, request.Note);
            if(model != null)
            {
                var totalPrice = request.UnitPrice * request.Quantity;
                var partList = new SalesPartList(request.PartId, request.Quantity, request.UOM, request.UnitPrice, totalPrice, request.Currency, null, null, false);
                partList.CreatedAT = DateTime.Now;
                partList.CreatedBy = request.CreatedBy;
                model.AddSalesPartList(partList);
            }
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _saleRepository.Add(model);
            var result = await _saleRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something went wrong when sales order created",
                };
            var returnData = new SalesQueryModel
            {
                Id = model.Id,
                CompanyId = model.CompanyId,
                OrderByName = model.OrderByName,
                OrderByEmail = model.OrderByEmail,
                OrderNo = model.OrderNo,
                CustomerOrderNo = model.CustomerOrderNo,
                ShipToAddress = model.ShipToAddress,
                Status = model.Status,
                Note = model.Note
            };
            return new ReturnDto<SalesQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Sales order created successfully"
            };
        }

    }
    public class CreateSalesCommand : IRequest<ReturnDto<SalesQueryModel>>
    {
        public Guid CompanyId { get; set; }
        public string? OrderByName { get; set; }
        public string? OrderByEmail { get; set; }
        public string? CustomerOrderNo { get; set; }
        public string? ShipToAddress { get; set; }
        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public double UnitPrice { get; set; }
        public string Currency { get; set; }

        public string Status { get; set; }
        public string? Note { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
