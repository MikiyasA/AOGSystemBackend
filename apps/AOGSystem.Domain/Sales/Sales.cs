using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public class Sales : BaseEntity
    {
        public int CompanyId { get; private set; }
        public string? OrderByName { get; private set; }
        public string? OrderByEmail { get; private set; }
        public string OrderNo { get; private set; }
        public string? CustomerOrderNo { get; private set; }
        public string? ShipToAddress { get; private set; }
        public string? Note { get; private set; }

        public void SetCompanyId(int companyId) { this.CompanyId = companyId; }
        public void SetOrderByName(string? orderName) { this.OrderByName = orderName; }
        public void SetOrderByEmail(string email) { this.OrderByEmail = email; }
        public void SetOrderNo(string orderNo) { this.OrderNo = orderNo; }
        public void SetCustomerOrderNo(string customerOrderNo) { this.CustomerOrderNo = customerOrderNo; }
        public void SetShipToAddress(string shipToAddress) { this.ShipToAddress = shipToAddress; }
        public void SetNote(string note) { this.Note = note; }

        private readonly List<SalesPartList> salesPartLists;
        public IReadOnlyCollection<SalesPartList> SalesPartLists => salesPartLists;

        protected Sales() : base() 
        {
            salesPartLists= new List<SalesPartList>();
        }

        public Sales(int companyId, string? orderByName, string? orderByEmail, string orderNo, string? customerOrderNo, string? shipToAddress, string? note) : this()
        {
            this.SetCompanyId(companyId);
            this.SetOrderByName(orderByName);
            this.SetOrderByEmail(orderByEmail);
            this.SetOrderNo(orderNo);
            this.SetCustomerOrderNo(customerOrderNo);
            this.SetShipToAddress(shipToAddress);
            this.SetNote(note);
        }

        public void AddSalesPartList(SalesPartList salesPartList)
        {
            salesPartLists.Add(salesPartList);
        }

        public void AddSalesPartList(int partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, bool isDeleted)
        {
            var newItem = new SalesPartList(partId, quantity, uom, unitPrice, totalPrice, currency, rid, serialNo, isDeleted);
            AddSalesPartList(newItem);
        }

        public void UpdateSalesPartList(int id, int partId, int quantity, string uom, int unitPrice, int totalPrice, string currency, string rid, string serialNo, bool isDeleted)
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

        public void RemoveSalesPartList(int id)
        {
            var existing = salesPartLists.FirstOrDefault(s => s.Id == id);
            if(existing != null)
            {
                RemoveSalesPartList(existing);
            }
        }



        // listpart 
    }
}
