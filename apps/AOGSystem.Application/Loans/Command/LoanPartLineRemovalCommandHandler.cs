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
    public class LoanPartLineRemovalCommandHandler : IRequestHandler<LoanPartLineRemovalCommand, ReturnDto<LoanPartListQueryModel>>
    {
        private readonly ILoanPartListRepository _loanPartListRepository;
        public LoanPartLineRemovalCommandHandler(ILoanPartListRepository loanPartListRepository)
        {
            _loanPartListRepository = loanPartListRepository;
        }
        public async Task<ReturnDto<LoanPartListQueryModel>> Handle(LoanPartLineRemovalCommand request, CancellationToken cancellationToken)
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

            model.SetIsDeleted(request.IsDeleted);
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

            var message = model.IsDeleted ? "Loan Part Line deleted successfully" : "Loan Part Line undeleted successfully";
            return new ReturnDto<LoanPartListQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = message
            };
        }
    }
    public class LoanPartLineRemovalCommand : IRequest<ReturnDto<LoanPartListQueryModel>>
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
}
