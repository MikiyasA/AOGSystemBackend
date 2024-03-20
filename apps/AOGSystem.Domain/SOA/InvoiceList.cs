using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.SOA
{
    public class InvoiceList : BaseEntity
    {
        public Guid VendorId { get; private set; }
        public string InvoiceNo { get; private set; }
        public string PONo { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public double Amount { get; set; }
        public string Currency { get; private set; }
        public string? UnderFollowup { get; private set; }
        public DateTime? PaymentProcessedDate { get; private set; }
        public DateTime? POPDate { get; private set; }
        public string? POPReference { get; private set; }
        public string? ChargeType { get; private set; }
        public string? BuyerName { get; private set; }
        public string? TLName { get; private set; }
        public string? ManagerName { get; private set; }
        public string Status { get; private set; }


        public void SetVendorId(Guid vendorId) { VendorId = vendorId; }
        public void SetInvoiceNo(string invoiceNo) { InvoiceNo = invoiceNo; }
        public void SetPONo(string poNo) {  PONo = poNo; }
        public void SetInvoiceDate(DateTime invoiceDate) {  InvoiceDate = invoiceDate; }
        public void SetDueDate(DateTime dueDate) {  DueDate = dueDate; }
        public void SetAmount(double amount) { Amount = amount; }
        public void SetCurrency(string currency) { Currency = currency; }
        public void SetUnderForllowup(string? underFollowup) { UnderFollowup = underFollowup; }
        public void SetPaymentProcessedDate(DateTime? paymentProcessedDate) { PaymentProcessedDate = paymentProcessedDate; }
        public void SetPOPDate(DateTime? popDate) { POPDate = popDate; }
        public void SetPOPReference(string popReference) { POPReference = popReference; }
        public void SetChargeTyoe(string chargeType) { ChargeType = chargeType; }
        public void SetBuyerName(string buyerName) { BuyerName = buyerName; }
        public void SetTLName(string tLName) { TLName = tLName; }
        public void SetManagerName(string managerName) { ManagerName = managerName; }
        public void SetStatus(string status) { Status = status; }

        private readonly List<FinanceRemark> financeRemarks;
        public IReadOnlyCollection<FinanceRemark> FinanceRemarks => financeRemarks;

        private readonly List<BuyerRemark> buyerRemarks;
        public IReadOnlyCollection<BuyerRemark> BuyerRemarks => buyerRemarks;

        protected InvoiceList()
        {
            financeRemarks = new List<FinanceRemark>();
            buyerRemarks = new List<BuyerRemark>();
        }

        public InvoiceList(string invoiceNo, string pONo, DateTime invoiceDate, DateTime dueDate, double amount, string currency, string? underForllowup, DateTime paymentProcessDate,
            DateTime popDate, string popReference, string chargeType, string buyerName, string tLName, string managerName, string status) : this()
        {
            InvoiceNo = invoiceNo;
            PONo = pONo;
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            Amount = amount;
            Currency = currency;
            UnderFollowup = underForllowup;
            PaymentProcessedDate = paymentProcessDate;
            POPDate = popDate;
            POPReference = popReference;
            ChargeType = chargeType;
            BuyerName = buyerName;
            TLName = tLName;
            ManagerName = managerName;
            Status = status;
        }
        public InvoiceList(string invoiceNo, string pONo, DateTime invoiceDate, DateTime dueDate, double amount, string currency, string? underForllowup, DateTime? paymentProcessedDate, DateTime? pOPDate,
            string? pOPReference, string? chargeType, string? buyerName, string? tLName, string? manegerName, string status) : this()
        {
            InvoiceNo = invoiceNo;
            PONo = pONo;
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            Amount = amount;
            Currency = currency;
            UnderFollowup = underForllowup;
            PaymentProcessedDate = paymentProcessedDate;
            POPDate = pOPDate;
            POPReference = pOPReference;
            ChargeType = chargeType;
            BuyerName = buyerName;
            TLName = tLName;
            ManagerName = manegerName;
            Status = status;
        }

        public void AddFinanceRemark(FinanceRemark financeRemark)
        {
            financeRemarks.Add(financeRemark);
        }
        public void AddFinanceRemark(string message, DateTime createdAt, Guid? createdBy)
        {
            var newItem = new FinanceRemark(message);
            newItem.CreatedAT = createdAt;
            newItem.CreatedBy = createdBy;
            AddFinanceRemark(newItem);
        }

        public void UpdateFinanceRemark(Guid id, string message, DateTime updatedAt, Guid? updatedBy)
        {
            var exsts = financeRemarks.FirstOrDefault(x => x.Id == id);
            if(exsts != null)
            {
                exsts.SetMessage(message);
                exsts.UpdatedAT = updatedAt;
                exsts.UpdatedBy = updatedBy;
            }
        }

        public void AddBuyerRemark(BuyerRemark buyerRemark)
        {
            buyerRemarks.Add(buyerRemark);
        }
        public void AddBuyerRemark(string message, DateTime createdAt, Guid? createdBy)
        {
            var newItem = new BuyerRemark(message);
            newItem.CreatedAT = createdAt;
            newItem.CreatedBy = createdBy;
            AddBuyerRemark(newItem);
        }

        public void UpdateBuyerRemark(Guid id, string message, DateTime updatedAt, Guid? updatedBy)
        {
            var exsts = buyerRemarks.FirstOrDefault(x => x.Id == id);
            if (exsts != null)
            {
                exsts.SetMessage(message);
                exsts.UpdatedAT = updatedAt;
                exsts.UpdatedBy = updatedBy;
            }
        }
    }
}
