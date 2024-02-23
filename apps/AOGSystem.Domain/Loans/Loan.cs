using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public class Loan : BaseEntity
    {
        public string OrderNo { get; private set; }
        public Guid CompanyId { get; private set; }
        public string CustomerOrderNo { get; private set; }
        public string OrderedByName { get; private set; }
        public string? OrderedByEmail { get; private set; }
        public string? ShipToAddress { get; private set; }
        public string? Status { get; private set; }
        public bool IsApproved { get; private set; }
        public string? Note { get; private set; }

        public void SetOrderNo(string orderNo) { OrderNo = orderNo; }
        public void SetCompanyId(Guid companyId) { CompanyId = companyId; }
        public void SetCustomerOrderNo(string customerOrderNo) { CustomerOrderNo = customerOrderNo; }
        public void SetOrderedByName(string orderByName) { OrderedByName = orderByName; }
        public void SetOrderedByEmail(string orderByEmail) { OrderedByEmail = orderByEmail; }
        public void SetShipToAddress(string shipToAddress) { ShipToAddress = shipToAddress; }
        public void SetStatus(string status) { Status = status; }
        public void SetIsApproved(bool isApproved) { IsApproved = isApproved; }
        public void SetNote(string note) { Note = note; }


        private readonly List<LoanPartList> loanPartLists;
        public IReadOnlyCollection<LoanPartList> LoanPartLists => loanPartLists;
        protected Loan() : base() 
        {
            loanPartLists = new List<LoanPartList>();
        }
        public Loan(string orderNo, Guid companyId, string customerOrderNo, string orderedByName, string? orderedByEmail, string shipToAddress, string? status, bool isApproved, string? note) : this ()
        {
            SetOrderNo(orderNo);
            SetCompanyId(companyId);
            SetCustomerOrderNo(customerOrderNo);
            SetOrderedByName(orderedByName);
            SetOrderedByEmail(orderedByEmail);
            SetStatus(shipToAddress);
            SetStatus(status);
            SetIsApproved(isApproved);
            SetNote(note);
        }

        public void AddLoanPartList(LoanPartList loanPartList)
        {
            loanPartLists.Add(loanPartList);
        }

        public void AddLoanPartList(Guid partId, int quantity, string uOM, string? serialNo, string? rID, DateTime? shipDate, string? shippingReference, DateTime? receivedDate, string? receivingReference, string? receivingDefect, bool isDeleted, bool isInvoiced)
        {
            var newItem = new LoanPartList(partId, quantity, uOM, serialNo, rID, shipDate, shippingReference, receivedDate, receivingReference, receivingDefect, isDeleted, isInvoiced);
            AddLoanPartList(newItem);
        }

        public void UpdateLoanPartList(Guid id, Guid partId, int quantity, string uOM, string? serialNo, string? rID, DateTime? shipDate, string? shippingReference, DateTime? receivedDate, string? receivingReference, string? receivingDefect, bool isDeleted, bool isInvoiced)
        {
            var exists = loanPartLists.FirstOrDefault(x => x.Id == id);
            if(exists != null)
            {
                exists.SetPartId(partId);
                exists.SetQuantity(quantity);
                exists.SetUOM(uOM);
                exists.SetSerialNo(serialNo);
                exists.SetRID(rID); 
                exists.SetShipDate(shipDate);
                exists.SetShippingReference(shippingReference);
                exists.SetReceivedDate(receivedDate);
                exists.SetReceivingReference(receivingReference);
                exists.SetReceivingDefect(receivingDefect);
                exists.SetIsDeleted(isDeleted);
                exists.SetIsInvoiced(isInvoiced);
            }
        }

        public void RemoveLoanPartList(LoanPartList loanPartList)
        {
            loanPartLists.Remove(loanPartList);
        }

        public void RemoveLoanPartList(Guid id)
        {
            var exists = loanPartLists.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                RemoveLoanPartList(exists);
            }
        }
    }
}
