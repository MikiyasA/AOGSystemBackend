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
    public class CreateQuotationCommandHandler : IRequestHandler<CreateQuotationCommand, QuotationQueryModel>
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly IPartRepository _partRepository;
        private readonly ICompanyRepository _companyRepository;
        public CreateQuotationCommandHandler(IQuotationRepository quotationRepository, 
            IPartRepository partRepository,
            ICompanyRepository companyRepository)
        {
            _quotationRepository = quotationRepository;
            _partRepository = partRepository;
            _companyRepository = companyRepository;
        }

        public async Task<QuotationQueryModel> Handle(CreateQuotationCommand request, CancellationToken cancellationToken)
        {
            var company =  _companyRepository.GetCompanyByCode(request.CompanyCode).ElementAt(0);
            if (company == null)
                return null;            // TODO TOASK
            var model = new Quotation(request.Loan,
                request.Sales,
                request.Exchange,
                company.Id,
                request.RequestedByName,
                request.RequestedByEmail,
                request.RequestedByPhone);
            model.CreatedAT= DateTime.Now;
             _quotationRepository.Add(model);
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
    public class CreateQuotationCommand : IRequest<QuotationQueryModel>
    {
        public bool Loan { get; set; }
        public bool Sales { get; set; }
        public bool Exchange { get; set; }
        public string CompanyCode { get; set; }
        public string? RequestedByName { get; set; }
        public string? RequestedByEmail { get; set; }
        public string? RequestedByPhone { get; set; }

        public CreateQuotationCommand()
        {

        }

        public CreateQuotationCommand(bool loan, bool sales, bool exchange, 
            string companyCode, string requestedByName, string requestedByEmail, string requestedByPhone)
        {
            //PartNumber = partNumber; 
            //Description = description; 
            //StockNo = stockNo;
            //FinancialClass = finantialClass;
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
