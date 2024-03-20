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
    public class AddSalesPartListInSalesCommandHandler : IRequestHandler<AddSalesPartListInSalesCommand, ReturnDto<SalesPartListQueryModel>>
    {
        private readonly ISalePartListRepository _salePartListRepository;
        private readonly ISaleRepository _saleRepository;
        public AddSalesPartListInSalesCommandHandler(ISalePartListRepository salePartListRepository, ISaleRepository saleRepository)
        {
            _salePartListRepository = salePartListRepository;
            _saleRepository = saleRepository;
        }

        public async Task<ReturnDto<SalesPartListQueryModel>> Handle(AddSalesPartListInSalesCommand request, CancellationToken cancellationToken)
        {
            var model = await _saleRepository.GetSalesByIDAsync(request.SalesId);
            if (model == null)
                return new ReturnDto<SalesPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Sales order can not be found"
                };
            var totalPrice = request.Quantity * request.UnitPrice;
            var newPartList = new SalesPartList(request.PartId, request.Quantity, request.UOM, request.UnitPrice, totalPrice, request.Currency, request.RID, request.SerialNo, request.IsDeleted);
            newPartList.CreatedAT = DateTime.Now;
            newPartList.CreatedBy = request.CreatedBy;
            model.AddSalesPartList(newPartList);
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
                Id = newPartList.Id,
                PartId = newPartList.PartId,
                Quantity = newPartList.Quantity,
                UOM = newPartList.UOM,
                UnitPrice = newPartList.UnitPrice,
                TotalPrice = newPartList.TotalPrice,
                Currency = newPartList.Currency,
                RID = newPartList.RID,
                SerialNo = newPartList.SerialNo,
                IsDeleted = newPartList.IsDeleted
            };
            return new ReturnDto<SalesPartListQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = "Sales Part List added successfully"
            };

        }
    }
    public class AddSalesPartListInSalesCommand : IRequest<ReturnDto<SalesPartListQueryModel>>
    {
        public Guid SalesId { get; set; }
        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public string? RID { get; set; }
        public string? SerialNo { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
