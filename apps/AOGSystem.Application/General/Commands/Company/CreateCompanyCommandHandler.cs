using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Commands.Company
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyQueryModel>
    {
        readonly ICompanyRepository _companyRepository;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyQueryModel> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByCodeAsync(request.Code);
            if (company != null)
            { // TODO
                throw new Exception($"Company with code {request.Code} already exists");
            }

            var model = new Domain.General.Company(request.Name, request.Code, request.Address, request.City, request.Country, request.Phone,
                request.ShipToAddress, request.BillToAddress, request.PaymentTerm);
            model.CreatedAT = DateTime.Now;
            _companyRepository.Add(model);
            var result = await _companyRepository.SaveChangesAsync();
            if (result == 0)
                return null;
            return new CompanyQueryModel
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
        }
    }
    public class CreateCompanyCommand : IRequest<CompanyQueryModel>
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

        public CreateCompanyCommand() { }

        public CreateCompanyCommand(string? name, string? code, string? address, string? city, string? country, string? phone, string? shipToAddress, string? billToAddress, string? paymentTerm)
        {
            Name = name;
            Code = code;
            Address = address;
            City = city;
            Country = country;
            Phone = phone;
            ShipToAddress = shipToAddress;
            BillToAddress = billToAddress;
            PaymentTerm = paymentTerm;
        }
    }
}
