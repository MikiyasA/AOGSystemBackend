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
    public class CreateLoanCommanHandler : IRequestHandler<CreateLoanComman, ReturnDto<LoanQueryModel>>
    {
        private readonly ILoanRepository _loanRepository;
        public CreateLoanCommanHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<ReturnDto<LoanQueryModel>> Handle(CreateLoanComman request, CancellationToken cancellationToken)
        {
            var lastOrder = await _loanRepository.GetLastLoanOrder();
            int currentYear = DateTime.Now.Year;
            var nextOrderNo = lastOrder == null ? 1 : OrderUtility.GetNextOrderNo(lastOrder.OrderNo);

            var orderNo = $"L{currentYear}{nextOrderNo:D2}";

            var model = new Loan(orderNo, request.CompanyId, request.CustomerOrderNo, request.OrderedByName, request.OrderedByEmail, request.ShipToAddress, "Created", false, request.Note);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            var newPartlist = new LoanPartList(request.PartId, request.Quantity, request.UOM);

            foreach(var description in request.Description)
            {
                var unitPrice = OrderUtility.GetLoanUnitPrice(description, request.BasePrice, request.UnitPrice);
                var totalPrice = unitPrice * request.Quantity;
                var newOffer = new Offer(description, request.BasePrice, request.Quantity, unitPrice, totalPrice, request.Currency);
                newPartlist.AddOffer(newOffer);
            }
            model.AddLoanPartList(newPartlist);


            _loanRepository.Add(model);
            var result = await _loanRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<LoanQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when Loan order created",
                };
            var returnDate = new LoanQueryModel
            {
                Id = model.Id,
                OrderNo = model.OrderNo,
                CompanyId = model.CompanyId,
                CustomerOrderNo = model.CustomerOrderNo,
                OrderedByName = model.OrderedByName,
                OrderedByEmail = model.OrderedByEmail,
                Status = model.Status,
                IsApproved = model.IsApproved,
                Note = model.Note
            };
            return new ReturnDto<LoanQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = "Loan order created successfully"
            };
        }
    }
    public class CreateLoanComman : IRequest<ReturnDto<LoanQueryModel>>
    {
        public Guid CompanyId { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OrderedByName { get; set; }
        public string? OrderedByEmail { get; set; }
        public string? ShipToAddress { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public string? UOM { get; set; }

        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        
        public List<string> Description { get; set; }
        public double BasePrice { get; set; }
        public double? UnitPrice { get; set; }



        [JsonIgnore]
        public string? CreatedBy { get; private set; }
        public void SetCreatedBy(string createdBy) { CreatedBy = createdBy; }
    }
}
