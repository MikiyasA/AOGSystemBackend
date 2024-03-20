using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Sales.Query;
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
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, ReturnDto<VendorQueryModel>>
    {
        private readonly IVendorRepository _vendorRepository;
        public CreateVendorCommandHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<ReturnDto<VendorQueryModel>> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var model = new Vendor(request.VendorName, request.VendorCode, request.Address, request.VendorAccountManagerName, request.VendorAccountManagerEmail, request.VendorFinanceContactName,
                request.VendorFinanceContactEmail, request.CreditLimit, request.ETFinanceContactName, request.ETFinanceContactEmail, request.SOAHandlerBuyerId, request.SOAHandlerBuyerName,
                request.CertificateExpiryDate, request.AssessmentDate, request.Status, request.Remark);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _vendorRepository.Add(model);
            var result = await _vendorRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<VendorQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when vendor created",
                };

            var returnData = new VendorQueryModel
            {
                Id = model.Id,
                VendorName = model.VendorName,
                VendorCode = model.VendorCode,
                Address = model.Address,
                VendorAccountManagerName = model.VendorAccountManagerName,
                VendorAccountManagerEmail = model.VendorAccountManagerEmail,
                VendorFinanceContactName = model.VendorFinanceContactName,
                VendorFinanceContactEmail = model.VendorFinanceContactEmail,
                CreditLimit = model.CreditLimit,
                TotalOutstanding = model.TotalOutstanding,
                UnderProcess = model.UnderProcess,
                UnderDispute = model.UnderDispute,
                PaidAmount = model.PaidAmount,
                ETFinanceContactName = model.ETFinanceContactName,
                ETFinanceContactEmail = model.ETFinanceContactEmail,
                SOAHandlerBuyerId = model.SOAHandlerBuyerId,
                SOAHandlerBuyerName = model.SOAHandlerBuyerName,
                CertificateExpiryDate = model.CertificateExpiryDate,
                AssessmentDate = model.AssessmentDate,
                Status = model.Status,
                Remark = model.Remark,
            };

            return new ReturnDto<VendorQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Vendor successfully created",
            };
        }
    }

    public class CreateVendorCommand : IRequest<ReturnDto<VendorQueryModel>>
    {
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string Address { get; set; }
        public string? VendorAccountManagerName { get; set; }
        public string? VendorAccountManagerEmail { get; set; }
        public string? VendorFinanceContactName { get; set; }
        public string? VendorFinanceContactEmail { get; set; }
        public double? CreditLimit { get; set; }
        public string? ETFinanceContactName { get; set; }
        public string? ETFinanceContactEmail { get; set; }
        public Guid? SOAHandlerBuyerId { get;  set; }
        public string? SOAHandlerBuyerName { get;  set; }
        public DateTime CertificateExpiryDate { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
