using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Loans.Query;
using AOGSystem.Application.Sales.Query;
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
    public class UpdateLoanCommanHandler : IRequestHandler<UpdateLoanComman, ReturnDto<LoanQueryModel>>
    {
        private readonly ILoanRepository _loanRepository;
        public UpdateLoanCommanHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<ReturnDto<LoanQueryModel>> Handle(UpdateLoanComman request, CancellationToken cancellationToken)
        {
            var model = await _loanRepository.GetLoanByIDAsync(request.Id);
            if(model == null)
            {
                return new ReturnDto<LoanQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Loan order can not be found"
                };
            }
            model.SetCompanyId(request.CompanyId);
            model.SetCustomerOrderNo(request.CustomerOrderNo);
            model.SetOrderedByName(request.OrderedByName);
            model.SetOrderedByEmail(request.OrderedByEmail);
            model.SetStatus(request.Status);
            model.SetIsApproved(false);
            model.SetNote(request.Note);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _loanRepository.Update(model);
            var result = await _loanRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<LoanQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when loan order updated",
                };
            var returnData = new LoanQueryModel
            {
                Id = model.Id,
                OrderNo = model.OrderNo,
                CompanyId = model.CompanyId,
                CustomerOrderNo = model.CustomerOrderNo,
                OrderedByName = model.OrderedByName,
                OrderedByEmail = model.OrderedByEmail,
                Status = model.Status,
                IsApproved = model.IsApproved,
                Note = model.Note,
            };
            return new ReturnDto<LoanQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Loan order updated successfully"
            };
        }
    }
    public class UpdateLoanComman : IRequest<ReturnDto<LoanQueryModel>>
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int CompanyId { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OrderedByName { get; set; }
        public string? OrderedByEmail { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
