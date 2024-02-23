using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Invoices
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNo { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        public Guid? SalesOrderId { get; private set; }
        public Guid? LoanOrderId { get; private set; }
        public string TransactionType { get; private set; }
        public bool IsApproved { get; private set; }
        public string? POPReference { get; private set; } // POP - ProofOfPayment
        public DateTime? POPDate { get; private set; } // POP - ProofOfPayment
        public string Status { get; private set; }
        public string? Remark { get; private set; }

        public void SetInvoiceNo(string invoiceNo) { InvoiceNo = invoiceNo; }
        public void SetInvoiceDate(DateTime invoiceDate) { InvoiceDate = invoiceDate; }
        public void SetDueDate(DateTime? dueDate) { DueDate = dueDate; }
        public void SetSalesOrderId(Guid? salesOrderId) { SalesOrderId = salesOrderId; }
        public void SetLoanOrderId(Guid? loanOrderId) { LoanOrderId = loanOrderId; }
        public void SetTransactionType(string transactionType) { TransactionType = transactionType; }
        public void SetIsApproved(bool isApproved) { IsApproved = isApproved; }
        public void SetPOPReference(string popReference) { POPReference = popReference; }
        public void SetPOPDate(DateTime? popDate) { POPDate = popDate; }
        public void SetStatus(string status) { Status = status; }
        public void SetRemark(string? remark) { Remark = remark; }

        private readonly List<InvoicePartList> invoicePartLists;
        public IReadOnlyCollection<InvoicePartList> InvoicePartLists => invoicePartLists;

        protected Invoice() : base()
        {
            invoicePartLists = new List<InvoicePartList>();
        }

        public Invoice(string invoiceNo, DateTime invoiceDate, DateTime? dueDate, Guid? salesOrderId, Guid? loanOrderId, string transactionType, bool isApproved, string? pOPReference, DateTime? pOPDate,
            string status, string? remark) : this()
        {
            SetInvoiceNo(invoiceNo);
            SetInvoiceDate(invoiceDate);
            SetDueDate(dueDate);
            SetSalesOrderId(salesOrderId);
            SetLoanOrderId(loanOrderId);
            SetTransactionType(transactionType);
            SetIsApproved(isApproved);
            SetPOPReference(pOPReference);
            SetPOPDate(pOPDate);
            SetStatus(status);
            SetRemark(remark);
        }

        public void AddInvoicePartList(InvoicePartList salesPartList)
        {
            invoicePartLists.Add(salesPartList);
        }

        public void AddInvoicePartList(Guid partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, List<Offer>? offers)
        {
            var newItem = new InvoicePartList(partId, quantity, uom, unitPrice, totalPrice, currency, rid, serialNo, offers);
            AddInvoicePartList(newItem);
        }

        public void UpdateInvoicePartList(Guid id, Guid partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, bool isDeleted)
        {
            var existing = invoicePartLists.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                existing.SetPartId(partId);
                existing.SetQuantity(quantity);
                existing.SetUOM(uom);
                existing.SetUnitPrice(unitPrice);
                existing.SetTotalPrice(totalPrice);
                existing.SetCurrency(currency);
                existing.SetRID(rid);
                existing.SetSerialNo(serialNo);
            }
        }

        public void RemoveInvoicePartList(InvoicePartList salesPartList)
        {
            invoicePartLists.Remove(salesPartList);
        }

        public void RemoveInvoicePartList(Guid id)
        {
            var existing = invoicePartLists.FirstOrDefault(s => s.Id == id);
            if (existing != null)
            {
                RemoveInvoicePartList(existing);
            }
        }
    }
}
