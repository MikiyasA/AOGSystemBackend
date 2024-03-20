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
    public class AddInvoiceListCommandHandler : IRequestHandler<AddInvoiceListCommand, ReturnDto<InvoiceListQueryModel>>
    {
        private readonly IVendorRepository _vendorRepository;
        public AddInvoiceListCommandHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<ReturnDto<InvoiceListQueryModel>> Handle(AddInvoiceListCommand request, CancellationToken cancellationToken)
        {
            var model = await _vendorRepository.GetActiveVendorSOAByIDAsync(request.VendorId);
            if (model == null)
                return new ReturnDto<InvoiceListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Active Vendor could no be found to add invoice list",
                };
            var newList = new InvoiceList(request.InvoiceNo, request.PONo, request.InvoiceDate, request.DueDate, request.Amount, request.Currency, request.UnderFollowup, request.PaymentProcessedDate,
                request.POPDate, request.POPReference, request.ChargeType, request.BuyerName, request.TLName, request.ManagerName, request.Status);
            newList.CreatedAT = DateTime.Now;
            newList.CreatedBy = request.CreatedBy;

            model.AddInvoiceList(newList);

            _vendorRepository.Update(model);
            var result = await _vendorRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<InvoiceListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when invoice added",
                };

            var returnData = new InvoiceListQueryModel
            {
                Id = newList.Id,
                InvoiceNo = newList.InvoiceNo,
                PONo = newList.PONo,
                InvoiceDate = newList.InvoiceDate,
                DueDate = newList.DueDate,
                Amount = newList.Amount,
                Currency = newList.Currency,
                UnderFollowup = newList.UnderFollowup,
                PaymentProcessedDate = newList.PaymentProcessedDate,
                POPDate = newList.POPDate,
                POPReference = newList.POPReference,
                ChargeType = newList.ChargeType,
                BuyerName = newList.BuyerName,
                ManagerName = newList.ManagerName,
                Status = newList.Status,
            };

            return new ReturnDto<InvoiceListQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Invoice is added successfully created",
            };
        }
    }

    public class AddInvoiceListCommand : IRequest<ReturnDto<InvoiceListQueryModel>>
    {
        public Guid VendorId { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string? UnderFollowup { get; set; }
        public DateTime? PaymentProcessedDate { get; set; }
        public DateTime? POPDate { get; set; }
        public string? POPReference { get; set; }
        public string? ChargeType { get; set; }
        public string? BuyerName { get; set; }
        public string? TLName { get; set; }
        public string? ManagerName { get; set; }
        public string Status { get; set; }


        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
