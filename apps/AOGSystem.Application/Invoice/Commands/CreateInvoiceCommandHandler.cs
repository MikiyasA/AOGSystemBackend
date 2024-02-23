using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Invoice.Query.Model;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Invoice.Commands
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, ReturnDto<InvoiceQueryModel>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ISalePartListRepository _salePartListRepository;
        private readonly ILoanPartListRepository _loanPartListRepository;
        public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, ISalePartListRepository salePartListRepository, ILoanPartListRepository loanPartListRepository)
        {
            _invoiceRepository = invoiceRepository;
            _salePartListRepository = salePartListRepository;
            _loanPartListRepository = loanPartListRepository;
        }
        public async Task<ReturnDto<InvoiceQueryModel>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var lastInvoice = await _invoiceRepository.GetLastInvoice();
            var nextInvoice = lastInvoice == null ? 1 : OrderUtility.GetNextOrderNo(lastInvoice.InvoiceNo);
            var invoiceNo = $"I{nextInvoice:D6}";

            var invoiceDate = DateTime.Now;
            var dueDate = invoiceDate.AddDays(30);

            var status = "Created";



            var model = new Domain.Invoices.Invoice(invoiceNo, invoiceDate, dueDate, request.SalesOrderId, request.LoanOrderId, request.TransactionType, false,
                null, null, status, request.Remark);
            if (request.PartLists.Count() < 1)
                return new ReturnDto<InvoiceQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Atleast one part list should be selected to raise invoice.",
                };
            foreach (var partList in request.PartLists)
            {
                
                if(request.TransactionType == "Sales")
                {
                    var salesPartList = await _salePartListRepository.GetSalesPartListByIDAsync(partList.Id);
                    salesPartList.SetIsInvoiced(true);
                    _salePartListRepository.Update(salesPartList);
                    partList.Id = Guid.Empty;
                    model.AddInvoicePartList(partList);
                }
                if (request.TransactionType == "Loan")
                {
                    var loanPartList = await _loanPartListRepository.GetLoanPartListByIDAsync(partList.Id);
                    loanPartList.SetIsInvoiced(true);
                    _loanPartListRepository.Update(loanPartList);
                    var unitPrice = partList.Offers?.Sum(x => x.TotalPrice);
                    var totalPrice = unitPrice * partList.Quantity;
                    partList.SetUnitPrice(unitPrice);
                    partList.SetTotalPrice(totalPrice);
                    partList.Id = Guid.Empty;
                    model.AddInvoicePartList(partList);
                }
            }
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _invoiceRepository.Add(model);
            var result = await _invoiceRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<InvoiceQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when invoice created",
                };
            var returnDate = new InvoiceQueryModel
            {
                Id = model.Id,
                InvoiceNo = model.InvoiceNo,
                InvoiceDate = model.InvoiceDate,
                DueDate = model.DueDate,
                SalesOrderId = model.SalesOrderId,
                LoanOrderId = model.LoanOrderId,
                TransactionType = model.TransactionType,
                IsApproved = model.IsApproved,
                POPReference = model.POPReference,
                POPDate = model.POPDate,
                Status = model.Status,
                Remark = model.Remark
            };

            return new ReturnDto<InvoiceQueryModel>
            {
                Data = returnDate,
                Count = 1,
                IsSuccess = true,
                Message = "Invoice created successfully"
            };

        }

    }

    public class CreateInvoiceCommand : IRequest<ReturnDto<InvoiceQueryModel>>
    {
        public Guid? SalesOrderId { get; set; }
        public Guid? LoanOrderId { get; set; }
        public string? TransactionType { get; set; }
        public string? Remark { get; set; }

        public List<InvoicePartList> PartLists { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; private set; }
        public void SetCreatedBy(string createdBy) { CreatedBy = createdBy; }
    }

}