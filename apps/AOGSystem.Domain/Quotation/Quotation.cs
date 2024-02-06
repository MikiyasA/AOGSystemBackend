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
        public User? OfferedBy { get; private set; }
        public Guid? OfferedById { get; private set; }

        public void SetLoan(bool loan) { this.Loan = loan; }
        public void SetSales(bool sales) { this.Sales = sales; }
        public void SetExchange(bool exchange) { this.Exchange = exchange; }
        public void SetCompanyId(int companyId) { this.CompanyId = companyId; }
        public void SetRequestedByName(string requestedByName) { this.RequestedByName = requestedByName; }
        public void SetRequestedByEmail(string requestedByEmail) { this.RequestedByEmail = requestedByEmail; }
        public void SetRequestedByPhone(string requestedByPhone) { this.RequestedByPhone = requestedByPhone; }
        public void SetOfferedById(Guid offeredById) { this.OfferedById = offeredById; }


        private readonly List<QuotationPartList> quotationPartLists;

        public IReadOnlyCollection<QuotationPartList> QuotationPartsLists => quotationPartLists;

        protected Quotation() : base()
        {
            quotationPartLists = new List<QuotationPartList>();
        }

        public Quotation(bool loan, bool sales, bool exchange, int companyId, string requestedByName, string requestedByEmail, string requestedByPhone) : this()
        {
            this.SetLoan(loan);
            this.SetSales(sales);
            this.SetExchange(exchange);
            this.SetCompanyId(companyId);
            this.SetRequestedByName(requestedByName);
            this.SetRequestedByEmail(requestedByEmail);
            this.SetRequestedByPhone(requestedByPhone);
        }

        public void AddQuotationPartList(QuotationPartList qPartList)
        {
            quotationPartLists.Add(qPartList);
        }

        public void AddQuotationPartList(int partId, decimal currentPrice, decimal salesPrice, decimal fixedLoanPrice, decimal loanPricePerDay,
            decimal exchangePrice, string? stockLocation, string? condition, string? serialNumber)
        {
            var newQuotationPartList = new QuotationPartList(partId, currentPrice, salesPrice, fixedLoanPrice, loanPricePerDay, exchangePrice,
                stockLocation, condition, serialNumber);
            quotationPartLists.Add(newQuotationPartList);
        }

        public void AddQuotationPartList(string partNumber, string description, string stockNo, string financialClass, decimal currentPrice, 
            decimal salesPrice, decimal fixedLoanPrice, decimal loanPricePerDay, decimal exchangePrice, string? stockLocation, string? condition, 
            string? serialNumber, string manufacurer, string type)
        {
            var newPart = new Part(partNumber, description, stockNo, financialClass, manufacurer, type);
            var newQuotationPartList = new QuotationPartList(newPart.Id, currentPrice, salesPrice, fixedLoanPrice, loanPricePerDay, exchangePrice,
                stockLocation, condition, serialNumber);
            quotationPartLists.Add(newQuotationPartList);
        }

        public void UpdateQuotationPartList(int id, int partId, decimal currentPrice, decimal salesPrice, decimal fixedLoanPrice, decimal loanPricePerDay, 
            decimal exchangePrice, string stockLocation, string condition, string serialNo)
        {
            var exists = quotationPartLists.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                exists.SetPartId(partId);
                exists.SetCurrentPrice(currentPrice);
                exists.SetSalesPrice(salesPrice); 
                exists.SetFixedLoanPrice(fixedLoanPrice);
                exists.SetLoanPricePerDay(loanPricePerDay);
                exists.SetExchangePrice(exchangePrice);
                exists.SetStockLocation(stockLocation);
                exists.SetCondition(condition);
                exists.SetSerialNumber(serialNo);
                exists.UpdatedAT = DateTime.UtcNow;
            }
        }

        public void RemoveQuotationPartList(QuotationPartList quotationPartList)
        {
            quotationPartLists.Remove(quotationPartList);
        }

        public void RemoveQuotationPartList(int id)
        {
            var exists = quotationPartLists.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                quotationPartLists.Remove(exists);
            }
        }

        public void RemoveQuotationPartListFromQuotation(int quotationId)
        {
            // TODO
        }

    }
}
