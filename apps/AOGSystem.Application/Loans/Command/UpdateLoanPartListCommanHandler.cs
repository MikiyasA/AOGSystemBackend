using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Loans.Query;
using AOGSystem.Domain.Loans;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Command
{
    public class UpdateLoanPartListCommanHandler : IRequestHandler<UpdateLoanPartListCommand, ReturnDto<LoanPartListQueryModel>>
    {
        private readonly ILoanPartListRepository _loanPartListRepository;
        public UpdateLoanPartListCommanHandler(ILoanPartListRepository loanPartListRepository)
        {
            _loanPartListRepository = loanPartListRepository;
        }
        public async Task<ReturnDto<LoanPartListQueryModel>> Handle(UpdateLoanPartListCommand request, CancellationToken cancellationToken)
        {
            var model = await _loanPartListRepository.GetLoanPartListByIDAsync(request.Id);
            if (model == null)
            {
                return new ReturnDto<LoanPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Loan Part List can not be found"
                };
            }
            model.SetPartId(request.PartId);
            model.SetQuantity(request.Quantity);
            model.SetUOM(request.UOM);
            model.SetSerialNo(request.SerialNo);
            model.SetRID(request.RID);
            model.SetShipDate(request.ShipDate);
            model.SetShippingReference(request.ShippingReference);
            model.SetReceivedDate(request.ReceivedDate);
            model.SetReceivingReference(request.ReceivingReference);
            model.SetReceivingDefect(request.ReceivingDefect);
            model.SetIsDeleted(request.IsDeleted);
            model.SetIsInvoiced(request.IsInvoiced);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _loanPartListRepository.Update(model);
            var result = await _loanPartListRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<LoanPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when Loan Part List updated",
                };
            var returnData = new LoanPartListQueryModel
            {
                Id = model.Id,
                PartId = model.PartId,
                Quantity = model.Quantity,
                UOM = model.UOM,
                SerialNo = model.SerialNo,
                RID = model.RID,
                ShipDate = model.ShipDate,
                ShippingReference = model.ShippingReference,
                ReceivedDate = model.ReceivedDate,
                ReceivingReference = model.ReceivingReference,
                ReceivingDefect = model.ReceivingDefect,
                IsDeleted = model.IsDeleted,
                IsInvoiced = model.IsInvoiced,
            };
            return new ReturnDto<LoanPartListQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Loan Part List updated successfully"
            };

        }
    }
    public class UpdateLoanPartListCommand : IRequest<ReturnDto<LoanPartListQueryModel>>
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public string? SerialNo { get; set; }
        public string? RID { get; set; }
        public DateTime? ShipDate { get; set; }
        public string? ShippingReference { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? ReceivingReference { get; set; }
        public string? ReceivingDefect { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsInvoiced { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
