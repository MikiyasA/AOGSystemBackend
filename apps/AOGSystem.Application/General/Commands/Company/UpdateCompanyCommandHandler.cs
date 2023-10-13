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
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyQueryModel>
    {
        private readonly ICompanyRepository _companyRepository;
        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyQueryModel> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var model = await _companyRepository.GetCompanyByIDAsync(request.Id);
            model.SetName(request.Name);
            model.SetCode(request.Code);
            model.SetAddress(request.Address);
            model.SetCity(request.City);
            model.SetCountry(request.Country);
            model.SetPhone(request.Phone);
            model.SetShipToAddres(request.ShipToAddress);
            model.SetBillToAddress(request.BillToAddress);
            model.SetPaymentTerm(request.PaymentTerm);
            model.UpdatedAT = DateTime.UtcNow;

            _companyRepository.Update(model);

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
    public class UpdateCompanyCommand : IRequest<CompanyQueryModel>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? ShipToAddress { get; set; }
        public string? BillToAddress { get; set; }
        public string? PaymentTerm { get; set; }

        public UpdateCompanyCommand() { }

        public UpdateCompanyCommand(int id, string? name, string? code, string? address, string? city, string? country, string? phone, string? shipToAddress, string? billToAddress, string? paymentTerm)
        {
            Id = id;
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
