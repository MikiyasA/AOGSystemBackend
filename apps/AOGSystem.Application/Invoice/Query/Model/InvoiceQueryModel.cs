using AOGSystem.Domain.General;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;

namespace AOGSystem.Application.Invoice.Query.Model
{
    public class InvoiceQueryModel
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? SalesOrderId { get; set; }
        public Guid? LoanOrderId { get; set; }
        public string TransactionType { get; set; }
        public bool IsApproved { get; set; }
        public string? POPReference { get; set; } // POP - ProofOfPayment
        public DateTime? POPDate { get; set; } // POP - ProofOfPayment
        public string Status { get; set; }
        public string? Remark { get; set; }
    }

    public class ActiveInvoicesQueryModel
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Domain.Sales.Sales? SalesOrder { get; set; }
        public Loan? LoanOrder { get; set; }
        public Company? Company { get; set; }
        public IReadOnlyCollection<InvoicePartList> InvoicePartLists { get; set; }
        public string TransactionType { get; set; }
        public bool IsApproved { get; set; }
        public string? POPReference { get; set; } // POP - ProofOfPayment
        public DateTime? POPDate { get; set; } // POP - ProofOfPayment
        public string Status { get; set; }
        public string? Remark { get; set; }
    }
}
