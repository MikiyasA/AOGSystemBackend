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
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, ReturnDto<CompanyQueryModel>>
    {
        readonly ICompanyRepository _companyRepository;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ReturnDto<CompanyQueryModel>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company =  _companyRepository.GetSingleCompanyByCode(request.Code);
            if (company != null)
                return new ReturnDto<CompanyQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Company with this code already existed"
                };
            

            var model = new Domain.General.Company(request.Name, request.Code, request.Address, request.City, request.Country, request.Phone,
                request.ShipToAddress, request.BillToAddress, request.PaymentTerm);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _companyRepository.Add(model);
            var result = await _companyRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<CompanyQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Something went wrong when Company created"
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
                Message = "Company successfully created"
            };
        }
    }
    public class CreateCompanyCommand : IRequest<ReturnDto<CompanyQueryModel>>
    {
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
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }

    }
}
