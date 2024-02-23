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
    public class UpdateSalesPartListInSalesCommandHandler : IRequestHandler<UpdateSalesPartListInSalesCommand, ReturnDto<SalesPartListQueryModel>>
    {
        private readonly ISaleRepository _saleRepository;
        public UpdateSalesPartListInSalesCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<ReturnDto<SalesPartListQueryModel>> Handle(UpdateSalesPartListInSalesCommand request, CancellationToken cancellationToken)
        {
            var model = await _saleRepository.GetSalesByIDAsync(request.SalesId);
            if(model == null)
                return new ReturnDto<SalesPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Sales order can not be found",
                };
            var totalPrice = request.Quantity * request.UnitPrice;
            model.UpdateSalesPartList(request.Id, request.PartId, request.Quantity, request.UOM, request.UnitPrice, totalPrice, request.Currency, request.RID, request.SerialNo, request.IsDeleted);
            
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _saleRepository.Update(model);
            var result = await _saleRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<SalesPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when sales part list added",
                };

            var returnDate = new SalesPartListQueryModel
            {
                PartId = request.PartId,
                Quantity = request.Quantity,
                UOM = request.UOM,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.TotalPrice,
                Currency = request.Currency,
                RID = request.RID,
                SerialNo = request.SerialNo,
                IsDeleted = request.IsDeleted
            };
            return new ReturnDto<SalesPartListQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = "Sales order part list updated seccessfully"
            };
        }
    }
    public class UpdateSalesPartListInSalesCommand : IRequest<ReturnDto<SalesPartListQueryModel>>
    {
        public Guid Id { get; set; }
        public Guid SalesId { get; set; }
        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string Currency { get; set; }
        public string? RID { get; set; }
        public string? SerialNo { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
