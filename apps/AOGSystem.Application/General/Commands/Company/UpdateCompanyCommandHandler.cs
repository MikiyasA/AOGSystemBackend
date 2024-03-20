using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Company
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, ReturnDto<CompanyQueryModel>>
    {
        private readonly ICompanyRepository _companyRepository;
        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ReturnDto<CompanyQueryModel>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var model = await _companyRepository.GetCompanyByIDAsync(request.Id);
            if(model == null)
                return new ReturnDto<CompanyQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Company  could not be found"
                };
            model.SetName(request.Name);
            model.SetCode(request.Code);
            model.SetAddress(request.Address);
            model.SetCity(request.City);
            model.SetCountry(request.Country);
            model.SetPhone(request.Phone);
            model.SetShipToAddres(request.ShipToAddress);
            model.SetBillToAddress(request.BillToAddress);
            model.SetPaymentTerm(request.PaymentTerm);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _companyRepository.Update(model);

            var result = await _companyRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<CompanyQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something went wrong when Company updated"
                }; 

            var returnData = new CompanyQueryModel
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                Phone = model.Phone,
                ShipToAddress = model.ShipToAddress,
                BillToAddress = model.BillToAddress,
                PaymentTerm = model.PaymentTerm,
            };
            return new ReturnDto<CompanyQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Company successfully updated"
            };
        }
    }
    public class UpdateCompanyCommand : IRequest<ReturnDto<CompanyQueryModel>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ShipToAddress { get; set; }
        public string? BillToAddress { get; set; }
        public string? PaymentTerm { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
}
