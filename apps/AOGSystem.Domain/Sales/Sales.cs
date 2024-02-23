using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public class Sales : BaseEntity
    {
        public Guid CompanyId { get; private set; }
        public string? OrderByName { get; private set; }
        public string? OrderByEmail { get; private set; }
        public string OrderNo { get; private set; }
        public string? CustomerOrderNo { get; private set; }
        public string? ShipToAddress { get; private set; }
        public string Status { get; private set; }
        public string? Note { get; private set; }
        public bool IsApproved { get; private set; } = false;
        public bool IsFullyShipped { get; private set; } = false;
        public string? AWBNo { get; private set; }
        public DateTime? ShipDate { get; private set; }
        public bool ReceivedByCustomer { get; private set; } = false;
        public DateTime? ReceivedDate { get; private set; }

        public void SetCompanyId(Guid companyId) { CompanyId = companyId; }
        public void SetOrderByName(string? orderName) { OrderByName = orderName; }
        public void SetOrderByEmail(string email) { OrderByEmail = email; }
        public void SetOrderNo(string orderNo) { OrderNo = orderNo; }
        public void SetCustomerOrderNo(string customerOrderNo) { CustomerOrderNo = customerOrderNo; }
        public void SetShipToAddress(string shipToAddress) { ShipToAddress = shipToAddress; }
        public void SetStatus(string status) {  Status = status; }
        public void SetNote(string note) { Note = note; }
        public void SetIsApprove(bool approved) { IsApproved = approved; }
        public void SetIsFullyShipped(bool fullyShipped) {  IsFullyShipped = fullyShipped; }
        public void SetAWBNO(string? awb) { AWBNo = awb; }
        public void SetShipDate(DateTime? date) { ShipDate = date; }
        public void SetRecievedByCustomer(bool recieved) { ReceivedByCustomer = recieved; }
        public void SetReceivedDate(DateTime? date) { ReceivedDate = date; }

        private readonly List<SalesPartList> salesPartLists;
        public IReadOnlyCollection<SalesPartList> SalesPartLists => salesPartLists;

        protected Sales() : base() 
        {
            salesPartLists= new List<SalesPartList>();
        }

        public Sales(Guid companyId, string? orderByName, string? orderByEmail, string orderNo, string? customerOrderNo, string? shipToAddress, string status, string? note) : this()
        {
            SetCompanyId(companyId);
            SetOrderByName(orderByName);
            SetOrderByEmail(orderByEmail);
            SetOrderNo(orderNo);
            SetCustomerOrderNo(customerOrderNo);
            SetShipToAddress(shipToAddress);
            SetStatus(status);
            SetNote(note);
        }

        public void AddSalesPartList(SalesPartList salesPartList)
        {
            salesPartLists.Add(salesPartList);
        }

        public void AddSalesPartList(Guid partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, bool isDeleted)
        {
            var newItem = new SalesPartList(partId, quantity, uom, unitPrice, totalPrice, currency, rid, serialNo, isDeleted);
            AddSalesPartList(newItem);
        }

        public void UpdateSalesPartList(Guid id, Guid partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, bool isDeleted)
        {
            var existing = salesPartLists.FirstOrDefault(s => s.Id == id);
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
                existing.SetIsDeleted(isDeleted);
            }
        }

        public void RemoveSalesPartList(SalesPartList salesPartList)
        {
            salesPartLists.Remove(salesPartList);
        }

        public void RemoveSalesPartList(Guid id)
        {
            var existing = salesPartLists.FirstOrDefault(s => s.Id == id);
            if(existing != null)
            {
                RemoveSalesPartList(existing);
            }
        }


    }
}
