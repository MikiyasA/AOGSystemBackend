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
    public class LoanCloserCommandHandler : IRequestHandler<LoanCloserCommand, ReturnDto<LoanQueryModel>>
    {
        private readonly ILoanRepository _loanRepository;
        public LoanCloserCommandHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<ReturnDto<LoanQueryModel>> Handle(LoanCloserCommand request, CancellationToken cancellationToken)
        {
            var model = await _loanRepository.GetLoanByIDAsync(request.Id);
            if (model == null)
            {
                return new ReturnDto<LoanQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Loan order can not be found"
                };
            }
            model.SetStatus(request.Status);
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
                    Message = "Someting went wrong on loan order approval",
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
            var message = model.Status == "Closed" ? "Loan order closed successfully" : model.Status == "Re-Opened" ? "Loan order re-opened successfully" : "";
            return new ReturnDto<LoanQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = message
            };
        }
    }
    
    public class LoanCloserCommand : IRequest<ReturnDto<LoanQueryModel>>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
    
}
