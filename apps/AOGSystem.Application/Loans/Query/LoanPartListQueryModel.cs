using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Query
{
    public class LoanPartListQueryModel
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set;}
        public int Quantity { get; set;}
        public string UOM { get; set;}
        public string? SerialNo { get; set;}
        public string? RID { get; set;}
        public DateTime? ShipDate { get; set;}
        public string? ShippingReference { get; set;}
        public DateTime? ReceivedDate { get; set;}
        public string? ReceivingReference { get; set;}
        public string? ReceivingDefect { get; set;}
        public bool IsDeleted { get; set;}
        public bool IsInvoiced { get; set;}
    }
}
