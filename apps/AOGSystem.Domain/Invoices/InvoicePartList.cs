using AOGSystem.Domain.Loans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Invoices
{
    public class InvoicePartList : BaseEntity
    {
        public Guid PartId { get; private set; }
        //public int InvoiceId { get; private set; }
        public int Quantity { get; private set; }
        public string UOM { get; private set; }
        public double? UnitPrice { get; private set; }
        public double? TotalPrice { get; private set; }
        public string? Currency { get; private set; }
        public string? RID { get; private set; }
        public string? SerialNo { get; private set; }
        public List<Offer>? Offers { get; private set; }


        public void SetPartId(Guid partId) { this.PartId = partId; }
        //public void SetInvoiceId(int invoiceId) { this.InvoiceId = invoiceId; }
        public void SetQuantity(int quantity) { this.Quantity = quantity; }
        public void SetUOM(string uOM) { this.UOM = uOM; }
        public void SetUnitPrice(double? unitPrice) { this.UnitPrice = unitPrice; }
        public void SetTotalPrice(double? totalPrice) { this.TotalPrice = totalPrice; }
        public void SetCurrency(string currency) { this.Currency = currency; }
        public void SetRID(string rid) { this.RID = rid; }
        public void SetSerialNo(string serialNo) { this.SerialNo = serialNo; }
        public void SetOffers(List<Offer>? offers) { Offers = offers; }

        public InvoicePartList(Guid partId, int quantity, string uOM, double? unitPrice, double? totalPrice, string currency, string? rID, string? serialNo, List<Offer>? offers)
        {
            this.SetPartId(partId);
            this.SetQuantity(quantity);
            this.SetUOM(uOM);
            this.SetUnitPrice(unitPrice);
            this.SetTotalPrice(totalPrice);
            this.SetCurrency(currency);
            this.SetRID(rID);
            this.SetSerialNo(serialNo);
            this.SetOffers(offers);
        }
    }
}
