using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class FollowUpTabs : BaseEntity
    {
        public string Name { get; private set; }
        //public AOGFollowUp? FollowUp { get; private set; }
        public string? Color { get; private set; }
        public string? Status { get; private set; }

        public void SetName(string name) { Name= name; }
        public void SetColor(string color) { Color= color; }
        public void SetStatus(string status) { Status= status; }

        private readonly List<AOGFollowUp> followUps;
        public IReadOnlyCollection<AOGFollowUp> FollowUps => followUps;
        protected FollowUpTabs()
        {
            followUps = new List<AOGFollowUp>();
        }

        public FollowUpTabs(string name, string color, string status) : this()
        {
            this.SetName(name);
            this.SetColor(color);
            this.Status = status;
        }

        public void AddFollowUp(AOGFollowUp followUp)
        {
            followUps.Add(followUp);
        }

        public void UpdateFollowUp(AOGFollowUp followUp)
        {
            var exist = followUps.FirstOrDefault(x => x.Id == followUp.Id);
            if (exist != null)
            {
                exist.SetRID(followUp.RID);
                exist.SetRequestDate(followUp.RequestDate);
                exist.SetAirCraft(followUp.AirCraft);
                exist.SetTailNumber(followUp.TailNo);
                exist.SetWorkLocation(followUp.WorkLocation);
                exist.SetAOGStation(followUp.AOGStation);
                exist.SetCustomer(followUp.Customer);
                exist.SetPartId(followUp.PartId);
                exist.SetPONumber(followUp.PONumber);
                exist.SetOrderType(followUp.OrderType);
                exist.SetQuantity(followUp.Quantity);
                exist.SetUOM(followUp.UOM);
                exist.SetVendor(followUp.Vendor);
                exist.SetEDD(followUp.EDD);
                exist.SetStatus(followUp.Status);
                exist.SetAWBNo(followUp.AWBNo);
                exist.SetFlightNo(followUp.FlightNo);
                exist.SetNeedHigherMgntAttn(followUp.NeedHigherMgntAttn);
                exist.SetFollowUpTabsId(followUp.FollowUpTabsId);
            }
        }

    }
}
