using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Quotation
{
    public class QuotationPartList : BaseEntity
    {
        public Part? Part { get; private set; }
        public int PartId { get; private set; }
        public int QuotationId { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public decimal SalesPrice { get; private set; }
        public decimal FixedLoanPrice { get; private set; }
        public decimal LoanPricePerDay { get; private set; }
        public decimal ExchangePrice { get; private set; }
        public string? StockLocation { get; private set; }
        public string? Condition { get; private set;  }
        public string? SerialNumber { get; private set; }

        public void SetPartId(int partId) { PartId = partId; }
        public void SetQuotationId(int quotationId) { this.QuotationId = quotationId; }
        public void SetCurrentPrice(decimal currentPrice) { this.CurrentPrice = currentPrice; }
        public void SetSalesPrice(decimal salesPrice) { this.SalesPrice = salesPrice; }
        public void SetFixedLoanPrice(decimal fixedLoanPrice) { this.FixedLoanPrice = fixedLoanPrice; }
        public void SetLoanPricePerDay(decimal loanPricePerDay) { this.LoanPricePerDay = loanPricePerDay; }
        public void SetExchangePrice(decimal exchangePrice) { this.ExchangePrice = exchangePrice; }
        public void SetStockLocation(string stockLocation) { this.StockLocation = stockLocation; }
        public void SetCondition(string condition) { this.Condition = condition; }
        public void SetSerialNumber(string serialNumber) { this.SerialNumber = serialNumber; }


        private readonly List<Part> parts;

        public IReadOnlyCollection<Part> Parts => parts;

        protected QuotationPartList() : base() 
        {
            parts = new List<Part>();
        }

        public QuotationPartList(int partId, decimal currentPrice, decimal salesPrice, decimal fixedLoanPrice, decimal loanPricePerDay,
            decimal exchangePrice, string? stockLocation, string? condition, string? serialNumber)
        {
            this.SetPartId(partId);
            this.SetCurrentPrice(currentPrice);
            this.SetSalesPrice(salesPrice);
            this.SetFixedLoanPrice(fixedLoanPrice);
            this.SetLoanPricePerDay(loanPricePerDay);
            this.SetExchangePrice(exchangePrice);
            this.SetStockLocation(stockLocation);
            this.SetCondition(condition);
            this.SetSerialNumber(serialNumber);
        }

        public void AddPart(Part newPart)
        {
            parts.Add(newPart);
        }

        public void AddPart(string partNumber, string description, string stockNo, string financialClass)
        {
            var newPart = new Part(partNumber, description, stockNo, financialClass);
            parts.Add(newPart);
        }

        public void UpdatePart(int id, string partNumber, string description, string financialClass)
        {
            var exists = parts.FirstOrDefault(p => p.Id == id);
            if(exists != null)
            {
                exists.SetPartNumber(partNumber);
                exists.SetDescription(description);
                exists.SetFinancialClass(financialClass);
            }
        }

        public void RemovePart(Part partTBRemoved)
        {
            parts.Remove(partTBRemoved);
        }

        public void RemovePart(int id)
        {
            var partTBRemoved = parts.FirstOrDefault(p =>p.Id == id);
            if(partTBRemoved != null)
            {
                RemovePart(partTBRemoved);
            }
        }

        public void RemoveAllParts()
        {
            parts.Clear();
        }
    }
}
