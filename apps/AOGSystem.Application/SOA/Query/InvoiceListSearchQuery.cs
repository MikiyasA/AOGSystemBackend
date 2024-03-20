using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Query
{
    public class InvoiceListSearchQuery
    {
        public string? InvoiceNo { get; set; }
        public string? PONo { get; set; }
        public DateTime? InvoiceDateFrom { get; set; }
        public DateTime? InvoiceDateTo { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public DateTime? POPDateFrom { get; set; }
        public DateTime? POPDateTo { get; set; }
        public string? POPReference { get; set; }
        public string? UnderFollowup { get; set; }
        public string? ChargeType { get; set; }
        public string? BuyerName { get; set; }
        public string? TLName { get; set; }
        public string? ManagerName { get; set; }
        public string? Status { get; set; }
    }
}
