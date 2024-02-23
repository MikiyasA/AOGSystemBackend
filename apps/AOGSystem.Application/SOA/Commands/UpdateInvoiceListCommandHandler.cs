using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.SOA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Commands
{
    public class UpdateInvoiceListCommandHandler : IRequestHandler<UpdateInvoiceListCommand, ReturnDto<InvoiceListQueryModel>>
    {
        private readonly IInvoiceListRepository _invoiceListRepository;
        public UpdateInvoiceListCommandHandler(IInvoiceListRepository invoiceListRepository)
        {
            _invoiceListRepository = invoiceListRepository;
        }
        public async Task<ReturnDto<InvoiceListQueryModel>> Handle(UpdateInvoiceListCommand request, CancellationToken cancellationToken)
        {
            var model = await _invoiceListRepository.GetSOAInvoiceListByIDAsync(request.Id);
            if (model == null)
                return new ReturnDto<InvoiceListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Invoice could no be found",
                };

            model.SetInvoiceNo(request.InvoiceNo);
            model.SetPONo(request.PONo);
            model.SetInvoiceDate(request.InvoiceDate);
            model.SetDueDate(model.DueDate);
            model.SetAmount(request.Amount);
            model.SetCurrency(request.Currency);
            model.SetPaymentProcessedDate(request.PaymentProcessedDate);
            model.SetPOPDate(request.POPDate);
            model.SetPOPReference(request.POPReference);
            model.SetChargeTyoe(request.ChargeType);
            model.SetBuyerName(request.BuyerName);
            model.SetManagerName(request.ManagerName);
            model.SetStatus(request.Status);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _invoiceListRepository.Update(model);

            var result = await _invoiceListRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<InvoiceListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when invoice updated",
                };

            var returnData = new InvoiceListQueryModel
            {
                Id = model.Id,
                InvoiceNo = model.InvoiceNo,
                PONo = model.PONo,
                InvoiceDate = model.InvoiceDate,
                DueDate = model.DueDate,
                Amount = model.Amount,
                Currency = model.Currency,
                PaymentProcessedDate = model.PaymentProcessedDate,
                POPDate = model.POPDate,
                POPReference = model.POPReference,
                ChargeType = model.ChargeType,
                BuyerName = model.BuyerName,
                ManagerName = model.ManagerName,
                Status = model.Status,
            };

            return new ReturnDto<InvoiceListQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Invoice is updated successfully updated",
            };
        }
    }

    public class UpdateInvoiceListCommand : IRequest<ReturnDto<InvoiceListQueryModel>>
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get;  set; }
        public string PONo { get;  set; }
        public DateTime InvoiceDate { get;  set; }
        public DateTime DueDate { get;  set; }
        public int Amount { get; set; }
        public string Currency { get;  set; }
        public DateTime? PaymentProcessedDate { get;  set; }
        public DateTime? POPDate { get;  set; }
        public string? POPReference { get;  set; }
        public string? ChargeType { get;  set; }
        public string? BuyerName { get;  set; }
        public string? ManagerName { get;  set; }
        public string Status { get;  set; }


        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    
    }
}
