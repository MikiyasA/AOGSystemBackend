using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Query
{
    public class InvoiceListQueryModel
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; }
        public string PONo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
        public string? UnderForllowup { get; set; }
        public DateTime? PaymentProcessedDate { get; set; }
        public DateTime? POPDate { get; set; }
        public string? POPReference { get; set; }
        public string? ChargeType { get; set; }
        public string? BuyerName { get; set; }
        public string? ManagerName { get; set; }
        public string Status { get; set; }
    }
}
