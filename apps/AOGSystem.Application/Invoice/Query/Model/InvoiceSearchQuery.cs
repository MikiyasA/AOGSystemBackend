using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Invoice.Query.Model
{
    public class InvoiceSearchQuery
    {
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDateFrom { get; set; }
        public DateTime? InvoiceDateTo { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public string? TransactionType { get; set; }
        public string? POPReference { get; set; } 
        public DateTime? POPDateFrom { get; set; }
        public DateTime? POPDateTo { get; set; }
        public string? Status { get; set; }
    }
}
