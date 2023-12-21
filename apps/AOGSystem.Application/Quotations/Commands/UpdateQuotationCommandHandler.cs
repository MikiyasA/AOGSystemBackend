using AOGSystem.Application.Quotations.Query;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Commands
{
    public class UpdateQuotationCommandHandler : IRequestHandler<UpdateQuotationCommand, QuotationQueryModel>
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly ICompanyRepository _companyRepository;
        public UpdateQuotationCommandHandler(IQuotationRepository quotationRepository,
            ICompanyRepository companyRepository)
        {
            _quotationRepository = quotationRepository;
            _companyRepository = companyRepository;
        }
        public async Task<QuotationQueryModel> Handle(UpdateQuotationCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetCompanyByCodeAsync(request.CompanyCode);
            if (company == null)
                return null; // TODO TOASK
            var model = await _quotationRepository.GetQuotationByIdAsync(request.Id);
            if(model != null)
            {
                model.SetLoan(model.Loan);
                model.SetSales(model.Sales);
                model.SetExchange(model.Exchange);
                model.SetCompanyId(company.Id);
                model.SetRequestedByName(model.RequestedByName);
                model.SetRequestedByEmail(model.RequestedByEmail);
                model.SetRequestedByPhone(model.RequestedByPhone);
                model.UpdatedAT = DateTime.Now;
                _quotationRepository.Update(model);
            }

            var result = await _quotationRepository.SaveChangesAsync();

            if (result == 0)
                return null;

            return new QuotationQueryModel
            {
                Id = model.Id,
                Loan = request.Loan,
                Sales = model.Sales,
                Exchange = model.Exchange,
                CompanyCode = company.Code,
                RequestedByName = model.RequestedByName,
                RequestedByEmail = model.RequestedByEmail,
                RequestedByPhone = model.RequestedByPhone,
            };
        }
    }
    public class UpdateQuotationCommand : IRequest<QuotationQueryModel>
    {
        public int Id { get; set; }

        public bool Loan { get; set; }
        public bool Sales { get; set; }
        public bool Exchange { get; set; }
        public string CompanyCode { get; set; }
        public string? RequestedByName { get; set; }
        public string? RequestedByEmail { get; set; }
        public string? RequestedByPhone { get; set; }

        public UpdateQuotationCommand()
        {

        }

        public UpdateQuotationCommand(bool loan, bool sales, bool exchange,
            string companyCode, string requestedByName, string requestedByEmail, string requestedByPhone)
        {
            Loan = loan;
            Sales = sales;
            Exchange = exchange;
            CompanyCode = companyCode;
            RequestedByName = requestedByName;
            RequestedByEmail = requestedByEmail;
            RequestedByPhone = requestedByPhone;
        }
    }
}
