using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class AOGFollowUp : BaseEntity
    {
        public string? RID { get; private set; } // Request ID
        public DateTime RequestDate { get; private set; }
        public string? AirCraft { get; private set; }
        public string? TailNo { get; private set; }
        public string? WorkLocation { get; private set; }
        public string? AOGStation { get; private set; } 
        public string? Customer { get; private set; }
        public string? PONumber { get; private set; } // purchase order number 
        public string? OrderType { get;private set; }
        public int Quantity { get; private set; }
        public string? UOM { get;private set; } // unit of measurement
        public string? Vendor { get;private set; }
        public DateTime? EDD { get; private set; } // Estimated Deliver Date
        public string? Status { get; private set; }
        public string? AWBNo { get; private set; }
        public string FlightNo { get; private set; }
        public bool NeedHigherMgntAttn { get; private set; }
        public Part? Part { get; set; }
        public Guid PartId { get; set; }

        public Guid FollowUpTabsId { get; private set; }


        public void SetRID(string rid) { this.RID = rid; }
        public void SetRequestDate(DateTime requestDate) { this.RequestDate = requestDate; }
        public void SetAirCraft(string airCraft) { this.AirCraft = airCraft; }
        public void SetTailNumber(string tailNo) { this.TailNo = tailNo; }
        public void SetWorkLocation(string workLocation) { this.WorkLocation = workLocation; }
        public void SetAOGStation(string aogStation) { this.AOGStation= aogStation; }
        public void SetCustomer(string customer) { this.Customer = customer; }
        public void SetPartId(Guid partId) { this.PartId = partId; }
        public void SetPONumber(string pONumber) { this.PONumber = pONumber; }
        public void SetOrderType(string orderType) { this.OrderType = orderType; }
        public void SetQuantity(int quantity) { this.Quantity = quantity; }
        public void SetUOM(string uom) { this.UOM = uom; }
        public void SetVendor(string vendor) { this.Vendor = vendor; }
        public void SetEDD(DateTime? esd) { this.EDD = esd; }
        public void SetStatus(string status) { this.Status = status; }
        public void SetAWBNo(string awbNo) { this.AWBNo= awbNo; }
        public void SetFlightNo(string flightNo) { this.FlightNo = flightNo; }
        public void SetNeedHigherMgntAttn(bool needHigherMgntAttn) { this.NeedHigherMgntAttn = needHigherMgntAttn; }

        public void SetFollowUpTabsId(Guid followUpTabsId) { FollowUpTabsId = followUpTabsId; }


        //private readonly List<Part> parts;
        //public IReadOnlyCollection<Part> Parts => parts;



        private readonly List<Remark> remarks;
        public IReadOnlyCollection<Remark> Remarks => remarks;

        protected AOGFollowUp() 
        { 
            remarks = new List<Remark>();
            //parts = new List<Part>();
        }

        public AOGFollowUp(string rID, DateTime requestDate, string airCraft, string tailNo, string workLocation, string aogStation, 
            string customer, Guid partId, string pONumber, string? orderType, int quantity, string uOM, string vendor, DateTime? eSD, 
            string status, string awbNo, string flightNo, bool needHigherMgntAttn) : this ()
        {
            this.SetRID(rID);
            this.SetRequestDate(requestDate); 
            this.SetAirCraft(airCraft);
            this.SetTailNumber(tailNo);
            this.SetWorkLocation(workLocation);
            this.SetAOGStation(aogStation);
            this.SetCustomer(customer);
            this.SetPartId(partId);
            this.SetPONumber(pONumber);
            this.SetOrderType(orderType);
            this.SetQuantity(quantity);
            this.SetUOM(uOM);
            this.SetVendor(vendor);
            this.SetEDD(eSD);
            this.SetStatus(status);
            this.SetAWBNo(awbNo);
            this.SetFlightNo(flightNo);
            this.SetNeedHigherMgntAttn(needHigherMgntAttn);
        }


        public void AddRemark(Remark remark)
        {
            remarks.Add(remark);
        }

        public void AddRemark(Guid aogFPId, string message)
        {
            var remark = new Remark(aogFPId, message);
            remarks.Add(remark);
        }

        public void UpdateRemark(Guid id, Guid aogFPId, string message)
        {
            var exist = remarks.FirstOrDefault(x => x.Id == id);
            if (exist != null)
            {
                exist.SetAOGFollowUpId(aogFPId);
                exist.SetMessage(message);
                exist.UpdatedAT = DateTime.UtcNow;
            }
        }

        public void RemoveRemark(Remark remark)
        {
            remarks.Remove(remark);
        }
        public void RemoveRemark(Guid id) 
        {
            var exist = remarks.FirstOrDefault(x => x.Id == id);
            if (exist != null)
            {
                remarks.Remove(exist);
            }
        }

    }
}
