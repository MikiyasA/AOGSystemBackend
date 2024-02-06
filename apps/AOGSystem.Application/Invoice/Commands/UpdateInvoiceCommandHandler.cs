using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Invoice.Query.Model;
using AOGSystem.Domain.Invoices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Invoice.Commands
{
    public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, ReturnDto<InvoiceQueryModel>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public UpdateInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<ReturnDto<InvoiceQueryModel>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var model = await _invoiceRepository.GetInvoiceByIDAsync(request.Id);
            if (model == null)
                return new ReturnDto<InvoiceQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Invoice can not be found"
                };

            model.SetInvoiceNo(request.InvoiceNo);
            model.SetDueDate(request.DueDate);
            model.SetSalesOrderId(request.SalesOrderId);
            model.SetLoanOrderId(request.LoanOrderId);
            model.SetTransactionType(request.TransactionType);
            model.SetIsApproved(request.IsApproved);
            model.SetPOPReference(request.POPReference);
            model.SetPOPDate(request.POPDate);
            model.SetStatus(request.Status);
            model.SetRemark(request.Remark);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _invoiceRepository.Update(model);
            var result = await _invoiceRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<InvoiceQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when invoice updated",
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
                Message = "Invoice updated successfully"
            };


        }
    }

    public class UpdateInvoiceCommand : IRequest<ReturnDto<InvoiceQueryModel>>
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int? SalesOrderId { get; set; }
        public int? LoanOrderId { get; set; }
        public string TransactionType { get; set; }
        public bool IsApproved { get; set; }
        public string? POPReference { get; set; } // POP - ProofOfPayment
        public DateTime? POPDate { get; set; } // POP - ProofOfPayment
        public string Status { get; set; }
        public string? Remark { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
