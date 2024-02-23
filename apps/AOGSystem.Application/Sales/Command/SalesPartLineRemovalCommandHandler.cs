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
    public class SalesPartLineRemovalCommandHandler : IRequestHandler<SalesPartLineRemovalCommand, ReturnDto<SalesPartListQueryModel>>
    {
        private readonly ISalePartListRepository _salePartListRepository;
        public SalesPartLineRemovalCommandHandler(ISalePartListRepository salePartListRepository)
        {
            _salePartListRepository = salePartListRepository;
        }
        public async Task<ReturnDto<SalesPartListQueryModel>> Handle(SalesPartLineRemovalCommand request, CancellationToken cancellationToken)
        {
            var model = await _salePartListRepository.GetSalesPartListByIDAsync(request.Id);
            if (model == null)
                return new ReturnDto<SalesPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Sales order can not be found",
                };

            model.SetIsDeleted(request.IsDeleted);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _salePartListRepository.Update(model);
            var result = await _salePartListRepository.SaveChangesAsync();
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
                PartId = model.PartId,
                Quantity = model.Quantity,
                UOM = model.UOM,
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
                Currency = model.Currency,
                RID = model.RID,
                SerialNo = model.SerialNo,
                IsDeleted = model.IsDeleted
            };
            var message = model.IsDeleted ? "Sales Part Line deleted successfully" : "Sales Part Line undeleted successfully";
            return new ReturnDto<SalesPartListQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = message
            };

        }
    }
    public class SalesPartLineRemovalCommand : IRequest<ReturnDto<SalesPartListQueryModel>>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
