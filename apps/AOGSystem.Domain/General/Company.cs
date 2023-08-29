using AOGSystem.Domain.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public class Company : BaseEntity
    {
        //public int QuotationId { get; private set; }
        public int SalesOrderId { get; private set; }
        public int LoanOrderId { get; private set; }
        public int ExchangeOrderId { get; private set; }

        public string? Name { get; private set; }
        public string? Code { get; private set; }
        public string? Address { get; private set; }
        public string? City { get; private set; }
        public string? Country { get; private set; }
        public string? Phone { get; private set; }
        public string? ShipToAddress { get; private set;}
        public string? BillToAddress { get; private set;}
        public string? PaymentTerm { get; private set; }
        public ICollection<Quotation.Quotation>? Quotations { get; set; }


        //public void SetQuotationId(int quotationId) { this.QuotationId = quotationId;}
        public void SetSalesOrderId(int salesOrderId) { this.SalesOrderId = salesOrderId;}
        public void SetLoanOrderId(int loanOrderId) { this.LoanOrderId = loanOrderId;}
        public void SetExchangeOrderId(int exchangeOrderId) { this.ExchangeOrderId = exchangeOrderId;}
        public void SetName(string? name) { this.Name = name;}
        public void SetCode(string? code) { this.Code = code;}
        public void SetAddress(string? address) { this.Address = address;}
        public void SetCity(string? city) { this.City = city;}
        public void SetCountry(string? country) { this.Country = country;}
        public void SetPhone(string? phone) { this.Phone = phone;}
        public void SetShipToAddres(string shipToAddress) { this.ShipToAddress = shipToAddress;}
        public void SetBillToAddress(string billToAddress) { this.BillToAddress= billToAddress;}
        public void SetPaymentTerm(string paymentTerm) { this.PaymentTerm = paymentTerm;}
    }
}
