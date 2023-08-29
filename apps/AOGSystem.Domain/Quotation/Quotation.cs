using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Quotation
{
    public class Quotation : BaseEntity
    {
        public bool Loan { get; private set; }
        public bool Sales { get; private set; }
        public bool Exchange { get; private set; }
        public Company? Company { get; private set; }
        public int CompanyId { get; set; }
        public string? RequestedByName { get; private set; }
        public string? RequestedByEmail { get; private set; }
        public string? RequestedByPhone { get; private set; }
        public List<QuotationPartList>? PartList { get; private set; }
        // TODO: OfferedBy

        public void SetLoan(bool loan) { this.Loan = loan; }
        public void SetSales(bool sales) { this.Sales = sales; }
        public void SetExchange(bool exchange) { this.Exchange = exchange; }
        public void SetCompany(Company company) { this.Company = company; }
        public void SetRequestedByName(string requestedByName) { this.RequestedByName = requestedByName; }
        public void SetRequestedByEmail(string requestedByEmail) { this.RequestedByEmail = requestedByEmail; }
        public void SetRequestedByPhone(string requestedByPhone) { this.RequestedByPhone = requestedByPhone; }


        private readonly List<QuotationPartList> quotationPartList;

        public IReadOnlyCollection<QuotationPartList> QuotationPartsLists => quotationPartList;

        protected Quotation() : base()
        {
            quotationPartList = new List<QuotationPartList>();
        }

        public Quotation(int id, bool loan, bool sales, bool exchange, Company company, string requestedByName, string requestedByEmail, string requestedByPhone) : this()
        {
            this.Id = id;
            this.Loan = loan;
            this.Sales = sales;
            this.Exchange = exchange;
            this.Company = company;
            this.RequestedByName = requestedByName;
            this.RequestedByEmail = requestedByEmail;
            this.RequestedByPhone = requestedByPhone;
        }

        public void AddQuotationPartList(QuotationPartList qPartList)
        {
            quotationPartList.Add(qPartList);
        }

        public void AddQuotationPartList(Part part, decimal currentPrice, decimal salesPrice, decimal loanPrice, decimal exchangePrice, string? stockLocation, string? condition, string? serialNumber)
        {
            var newQuotationPartList = new QuotationPartList(part, currentPrice, salesPrice, loanPrice, exchangePrice, stockLocation, condition, serialNumber);
            quotationPartList.Add(newQuotationPartList);
        }

        public void AddQuotationPartList(string partNumber, string description, string financialClass, decimal currentPrice, decimal salesPrice, decimal loanPrice, decimal exchangePrice, string? stockLocation, string? condition, string? serialNumber)
        {
            var newPart = new Part(partNumber, description, financialClass);
            var newQuotationPartList = new QuotationPartList(newPart, currentPrice, salesPrice, loanPrice, exchangePrice, stockLocation, condition, serialNumber);
            quotationPartList.Add(newQuotationPartList);
        }


    }
}
