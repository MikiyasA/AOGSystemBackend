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
    public class InvoiceApprovalCommandHandler : IRequestHandler<InvoiceApprovalCommand, ReturnDto<InvoiceQueryModel>>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceApprovalCommandHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        public async Task<ReturnDto<InvoiceQueryModel>> Handle(InvoiceApprovalCommand request, CancellationToken cancellationToken)
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
            model.SetIsApproved(request.IsApproved);
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
                Message = "Invoice approved successfully"
            };
        }
    }

    public class InvoiceApprovalCommand : IRequest<ReturnDto<InvoiceQueryModel>>
    {
        public Guid Id { get; set; }
        public bool IsApproved { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
}
