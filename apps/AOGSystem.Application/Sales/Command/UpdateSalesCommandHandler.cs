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
    public class UpdateSalesCommandHandler : IRequestHandler<UpdateSalesCommand, ReturnDto<SalesQueryModel>>
    {
        private readonly ISaleRepository _saleRepository;
        public UpdateSalesCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<ReturnDto<SalesQueryModel>> Handle(UpdateSalesCommand request, CancellationToken cancellationToken)
        {
            var model = await _saleRepository.GetSalesByIDAsync(request.Id);
            if(model == null)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Sales order can not be found"
                };

            model.SetCompanyId(request.CompanyId);
            model.SetOrderByName(request.OrderByName);
            model.SetOrderByEmail(request.OrderByEmail);
            model.SetOrderNo(request.OrderNo);
            model.SetCustomerOrderNo(request.CustomerOrderNo);
            model.SetShipToAddress(request.ShipToAddress);
            model.SetStatus(request.Status);
            model.SetNote(request.Note);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _saleRepository.Update(model);
            var result = await _saleRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<SalesQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when sales order updated",
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
                Message = "Sales order updated successfully"
            };
        }
    }
    public class UpdateSalesCommand : IRequest<ReturnDto<SalesQueryModel>>
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string? OrderByName { get; set; }
        public string? OrderByEmail { get; set; }
        public string OrderNo { get; set; }
        public string? CustomerOrderNo { get; set; }
        public string? ShipToAddress { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
}
