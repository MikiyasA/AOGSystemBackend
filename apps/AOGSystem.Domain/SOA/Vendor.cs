using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.SOA
{
    public class Vendor : BaseEntity
    {
        public string VendorName { get; private set; }
        public string VendorCode { get; private set; }
        public string Address { get; private set; }
        public string? VendorAccountManagerName { get; private set; }
        public string? VendorAccountManagerEmail { get; private set; }
        public string? VendorFinanceContactName { get; private set; }
        public string? VendorFinanceContactEmail { get; private set; }
        public double? CreditLimit { get; private set; }
        public double? TotalOutstanding { get; private set; }
        public double? UnderProcess { get; private set; }
        public double? UnderDispute { get; private set; }
        public double? PaidAmount { get; private set; }
        public string? ETFinanceContactName { get; private set; }
        public string? ETFinanceContactEmail { get; private set; }
        public DateTime? CertificateExpiryDate { get; private set; }
        public DateTime? AssessmentDate { get; private set; }
        public string Status { get; private set; }
        public string? Remark { get; private set; }


        public void SetVendorName(string vendorName) { VendorName = vendorName; }
        public void SetVendorCode(string vendorCode) {  VendorCode = vendorCode; }
        public void SetAddress(string address) { Address = address; }
        public void SetVendorAccountManagerName(string? vendorAccountManagerName) { VendorAccountManagerName =  vendorAccountManagerName; }
        public void SetVendorAccountManagerEmail(string? vendorAccountManagerEmail) { VendorAccountManagerEmail = vendorAccountManagerEmail; }
        public void SetVendorFinanceContactName(string? vendorFinanceContactName) { VendorFinanceContactName =  vendorFinanceContactName; }
        public void SetVendorFinanceContactEmail(string? vendorFinanceContactEmail) { VendorFinanceContactEmail = vendorFinanceContactEmail; }
        public void SetCreditLimit(int? creditLimit) {  CreditLimit = creditLimit; }
        //public void SetTotalOutstanding(int? totalOutstanding) {  TotalOutstanding = totalOutstanding; }
        //public void SetUnderProcess(int? underProcess) {  UnderProcess = underProcess; }
        //public void SetUnderDispute(int? underDispute) {  UnderDispute = underDispute; }
        //public void SetPaidAmount(int? paidAmount) {  PaidAmount = paidAmount; }
        public void SetETFinanceContactName(string?  etFinanceContactName) { ETFinanceContactName =  etFinanceContactName; }
        public void SetETFinanceContactEmail(string? etFinanceContactEmail) { ETFinanceContactEmail = etFinanceContactEmail; }
        public void SetCertificateExpiryDate(DateTime? expiryDate) {  CertificateExpiryDate = expiryDate; }
        public void SetAssessmentDate(DateTime? assessmentDate) {  AssessmentDate = assessmentDate; }
        public void SetStatus(string? status) { Status = status; }
        public void SetRemark(string? remark) { Remark = remark; }

        public void UpdateFinancialData()
        {
            TotalOutstanding = invoiceLists.Where(x => x.Status.ToLower() != "closed").Sum(x => x.Amount);
            UnderProcess = invoiceLists.Where(x => x.Status == "Under Process").Sum(x => x.Amount);
            UnderDispute = invoiceLists.Where(x => x.Status == "Under Dispute").Sum(x => x.Amount);
            PaidAmount = invoiceLists.Where(x => x.Status == "Paid").Sum(x => x.Amount);

        }

        private readonly List<InvoiceList> invoiceLists;
        public IReadOnlyCollection<InvoiceList> InvoiceLists => invoiceLists;

        protected Vendor()
        {
            invoiceLists = new List<InvoiceList>();
        }




        public Vendor(string vendorName, string vendorCode, string address, string? vendorAccountManagerName, string? vendorAccountManagerEmail, string? vendorFinanceContactName,
            string? vendorFinanceContactEmail, double? creditLimit, string eTFinanceContactName, string eTFinanceContactEmail, DateTime certificateExpiryDate, DateTime assessmentDate, string status, string? remark) : this()
        {
            VendorName = vendorName;
            VendorCode = vendorCode;
            Address = address;
            VendorAccountManagerName = vendorAccountManagerName;
            VendorAccountManagerEmail = vendorAccountManagerEmail;
            VendorFinanceContactName = vendorFinanceContactName;
            VendorFinanceContactEmail = vendorFinanceContactEmail;
            CreditLimit = creditLimit;
            ETFinanceContactName = eTFinanceContactName;
            ETFinanceContactEmail = eTFinanceContactEmail;
            CertificateExpiryDate = certificateExpiryDate;
            AssessmentDate = assessmentDate;
            Status = status;
            Remark = remark;
        }

        public void AddInvoiceList(InvoiceList invoiceList)
        {
            invoiceLists.Add(invoiceList);
        }
        public void AddInvoiceList(string invoiceNo, string pONo, DateTime invoiceDate, DateTime dueDate, int amount, string currency, string? underFollowup, DateTime? paymentProcessedDate, DateTime? pOPDate,
            string? pOPReference, string? chargeType, string? buyerName, string tLName, string? manegerName, string status)
        {
            var newItem = new InvoiceList(invoiceNo, pONo, invoiceDate, dueDate, amount, currency, underFollowup, paymentProcessedDate, pOPDate, pOPReference, chargeType, buyerName, tLName, manegerName, status);
            AddInvoiceList(newItem);
        }

        public void UpdateInvoiceList(Guid id, string invoiceNo, string pONo, DateTime invoiceDate, DateTime dueDate, int amount, string currency, DateTime? paymentProcessedDate, DateTime? pOPDate,
            string? pOPReference, string? chargeType, string? buyerName, string? manegerName)
        {
            var exists = invoiceLists.FirstOrDefault(x => x.Id == id);
            if(exists != null)
            {
                exists.SetInvoiceNo(invoiceNo);
                exists.SetPONo(pONo); 
                exists.SetInvoiceDate(invoiceDate);
                exists.SetDueDate(dueDate);
                exists.SetAmount(amount);
                exists.SetCurrency(currency);
                exists.SetPaymentProcessedDate(paymentProcessedDate);
                exists.SetPOPDate(pOPDate);
                exists.SetPOPReference(pOPReference);
                exists.SetChargeTyoe(chargeType);
                exists.SetBuyerName(buyerName);
                exists.SetManagerName(manegerName);

            }
        }
    }
}
