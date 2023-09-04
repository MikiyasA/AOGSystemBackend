using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class HomeBaseFollowUp : BaseEntity
    {
        public string? RID { get; private set; } // Request ID
        public DateTime RequestDate { get; private set; }
        public string? AirCraft { get; private set; }
        public string? TailNo { get; private set; }
        public string? Customer { get; private set; }
        public string? PONumber { get; private set; } // purchase order number 
        public string? OrderType { get;private set; }
        public int Quantity { get; private set; }
        public string? UOM { get;private set; } // unit of measurement
        public string? Vendor { get;private set; }
        public string? ESD { get; private set; } // Estimated Deliver Date
        public bool NeedHigherMgntAttn { get; private set; }
        // public int RemarkId { get; private set; }
        public ICollection<Remark>? Remarks { get; private set; } 

        public void SetRID(string rid) { this.RID = rid; }
        public void SetRequestDate(DateTime requestDate) { this.RequestDate = requestDate; }
        public void SetAirCraft(string airCraft) { this.AirCraft = airCraft; }
        public void SetTailNumber(string tailNo) { this.TailNo = tailNo; }
        public void SetCustomer(string customer) { this.Customer = customer; }
        //public void SetPartId(int partId) { this.PartId = partId; }
        public void SetPONumber(string pONumber) { this.PONumber = pONumber; }
        public void SetOrderType(string orderType) { this.OrderType = orderType; }
        public void SetQuantity(int quantity) { this.Quantity = quantity; }
        public void SetUOM(string uom) { this.UOM = uom; }
        public void SetVendor(string vendor) { this.Vendor = vendor; }
        public void SetESD(string esd) { this.ESD = esd; }
        public void SetNeedHigherMgntAttn(bool needHigherMgntAttn) { this.NeedHigherMgntAttn = needHigherMgntAttn; }
        //public void SetRemarkId(int remarkId) { this.RemarkId = remarkId; }
       
        public Part? Part { get; private set; }

        public HomeBaseFollowUp(string? rID, DateTime requestDate, string? airCraft, string? tailNo, string? customer, 
            Part part, string? pONumber, string? orderType, int quantity, string? uOM, string? vendor, string? eSD, bool needHigherMgntAttn)
        {
            this.SetRID(rID);
            this.SetRequestDate(requestDate); 
            this.SetAirCraft(airCraft);
            this.SetTailNumber(tailNo);
            this.SetCustomer(customer);
            this.Part = part;
            this.SetPONumber(pONumber);
            this.SetOrderType(orderType);
            this.SetQuantity(quantity);
            this.SetUOM(uOM);
            this.SetVendor(vendor);
            this.SetESD(eSD);
            this.SetNeedHigherMgntAttn(needHigherMgntAttn);
        }

    }
}
