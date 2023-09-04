using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.HomeBase.Query.Model
{
    public class HomeBaseFollowUPSummery
    {
        public int Id { get; set; } 
        public string? RID { get; set; }
        public DateTime RequestDate { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? Customer { get;   set; }
        public Part Part { get;   set; }
        public string? PONumber { get;   set; }  
        public string? OrderType { get;   set; }
        public int Quantity { get;   set; }
        public string? UOM { get;   set; } 
        public string? Vendor { get;   set; }
        public string? ESD { get;   set; } 
        public bool NeedHigherMgntAttn { get;   set; }

        internal static HomeBaseFollowUp ToModel(HomeBaseFollowUPSummery item)
        {
            return new HomeBaseFollowUp(
                item.RID,
                item.RequestDate,
                item.AirCraft,
                item.TailNo,
                item.Customer,
                item.Part,
                item.PONumber,
                item.OrderType,
                item.Quantity,
                item.UOM,
                item.Vendor,
                item.ESD,
                item.NeedHigherMgntAttn);
        }
        internal static List<HomeBaseFollowUp> ToModel(List<HomeBaseFollowUPSummery> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }
    public class HomeBaseFollowUPQueryModel : HomeBaseFollowUPSummery
    {
        public List<RemarkSummery> Remarks { get; set; } = new List<RemarkSummery>();
    }
}
