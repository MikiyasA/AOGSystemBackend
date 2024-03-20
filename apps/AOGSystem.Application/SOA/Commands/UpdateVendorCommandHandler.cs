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
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, ReturnDto<VendorQueryModel>>
    {
        private readonly IVendorRepository _vendorRepository;
        public UpdateVendorCommandHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }
        public async Task<ReturnDto<VendorQueryModel>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var model = await _vendorRepository.GetActiveVendorSOAByIDAsync(request.Id);
            if(model == null)
                return new ReturnDto<VendorQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Vendor could no be found to update",
                };

            model.SetVendorName(request.VendorName);
            model.SetVendorCode(request.VendorCode);
            model.SetAddress(request.Address);
            model.SetVendorAccountManagerName(request.VendorAccountManagerName);
            model.SetVendorAccountManagerEmail(request.VendorAccountManagerEmail);
            model.SetVendorFinanceContactName(request.VendorFinanceContactName);
            model.SetVendorFinanceContactEmail(request.VendorFinanceContactEmail);
            model.SetCreditLimit(request.CreditLimit);
            model.SetETFinanceContactName(request.ETFinanceContactName);
            model.SetETFinanceContactEmail(request.ETFinanceContactEmail);
            model.SetSOAHandlerBuyerId(request.SOAHandlerBuyerId);
            model.SetSOAHandlerBuyerName(request.SOAHandlerBuyerName);
            model.SetCertificateExpiryDate(request.CertificateExpiryDate);
            model.SetAssessmentDate(request.AssessmentDate);
            model.SetStatus(request.Status);
            model.SetRemark(request.Remark);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdateBy;

            _vendorRepository.Update(model);
            var result = await _vendorRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<VendorQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something went wrong when vendor created",
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
    public class UpdateVendorCommand : IRequest<ReturnDto<VendorQueryModel>>
    {
        public Guid Id { get; set; }
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string Address { get; set; }
        public string? VendorAccountManagerName { get; set; }
        public string? VendorAccountManagerEmail { get; set; }
        public string? VendorFinanceContactName { get; set; }
        public string? VendorFinanceContactEmail { get; set; }
        public int? CreditLimit { get; set; }
        public int? TotalOutstanding { get; set; }
        public int? UnderProcess { get; set; }
        public int? UnderDispute { get; set; }
        public int? PaidAmount { get; set; }
        public string? ETFinanceContactName { get; set; }
        public string? ETFinanceContactEmail { get; set; }
        public Guid? SOAHandlerBuyerId { get; set; }
        public string? SOAHandlerBuyerName { get; set; }
        public DateTime CertificateExpiryDate { get; set; }
        public DateTime AssessmentDate { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }

        [JsonIgnore]
        public Guid? UpdateBy { get; private set; }
        public void SetUpdateBy(Guid updateBy) { UpdateBy = updateBy; }
    }
}
