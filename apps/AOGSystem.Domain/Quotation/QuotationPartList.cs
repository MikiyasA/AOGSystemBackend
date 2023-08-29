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
        public int PartId { get;  set; }
        public decimal CurrentPrice { get; private set; }
        public decimal SalesPrice { get; private set; }
        public decimal LoanPrice { get; private set; }
        public decimal ExchangePrice { get; private set; }
        public string? StockLocation { get; private set; }
        public string? Condition { get; private set;  }
        public string? SerialNumber { get; private set; }

        public void SetCurrentPrice(decimal currentPrice) { this.CurrentPrice = currentPrice; }
        public void SetSalesPrice(decimal salesPrice) { this.SalesPrice = salesPrice; }
        public void SetLoanPrice(decimal loanPrice) { this.LoanPrice = loanPrice; }
        public void SetExchangePrice(decimal exchangePrice) { this.ExchangePrice = exchangePrice; }
        public void SetStockLocation(string stockLocation) { this.StockLocation = stockLocation; }
        public void SetCondition(string condition) { this.Condition = condition; }
        public void SetSerialNumber(string serialNumber) { this.SerialNumber = serialNumber; }


        private readonly List<Part> part;

        public IReadOnlyCollection<Part> Parts => part;

        protected QuotationPartList() : base() 
        {
            part = new List<Part>();
        }

        public QuotationPartList(Part part, decimal currentPrice, decimal salesPrice, decimal loanPrice, decimal exchangePrice, string? stockLocation, string? condition, string? serialNumber)
        {
            this.CurrentPrice = currentPrice;
            this.SalesPrice = salesPrice;
            this.LoanPrice = loanPrice;
            this.ExchangePrice = exchangePrice;
            this.StockLocation = stockLocation;
            this.Condition = condition;
            this.SerialNumber = serialNumber;
            this.Part =  part;
        }

        public void AddPart(Part newPart)
        {
            part.Add(newPart);
        }

        public void AddPart(string partNumber, string description, string financialClass)
        {
            var newPart = new Part(partNumber, description, financialClass);
            part.Add(newPart);
        }

        public void UpdatePart(int id, string partNumber, string description, string financialClass)
        {
            var exists = part.FirstOrDefault(p => p.Id == id);
            if(exists != null)
            {
                exists.SetPartNumber(partNumber);
                exists.SetDescription(description);
                exists.SetFinancialClass(financialClass);
            }
        }

        public void RemovePart(Part partTBRemoved)
        {
            part.Remove(partTBRemoved);
        }

        public void RemovePart(int id)
        {
            var partTBRemoved = part.FirstOrDefault(p =>p.Id == id);
            if(partTBRemoved != null)
            {
                RemovePart(partTBRemoved);
            }
        }

        public void RemoveAllParts()
        {
            part.Clear();
        }
    }
}
