using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class AOGFollowUPSummery
    {
        public int Id { get; set; }
        public string? RID { get; set; }
        public DateTime RequestDate { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? WorkLocation { get; set; }
        public string? AOGStation { get; set; }
        public string? Customer { get; set; }
        public int PartId { get; set; }
        public string? PONumber { get; set; }
        public string? OrderType { get; set; }
        public int Quantity { get; set; }
        public string? UOM { get; set; }
        public string? Vendor { get; set; }
        public DateTime? EDD { get; set; }
        public string? Status { get; set; }
        public string? AWBNo { get; set; }
        public bool NeedHigherMgntAttn { get; set; }
        public Remark Remark { get; set; }

        public static AOGFollowUp ToModel(AOGFollowUPSummery item)
        {
            return new AOGFollowUp(
                item.RID,
                item.RequestDate,
                item.AirCraft,
                item.TailNo,
                item.WorkLocation,
                item.AOGStation,
                item.Customer,
                item.PartId,
                item.PONumber,
                item.OrderType,
                item.Quantity,
                item.UOM,
                item.Vendor,
                item.EDD,
                item.Status,
                item.AWBNo,
                item.NeedHigherMgntAttn);
        }
        internal static List<AOGFollowUp> ToModel(List<AOGFollowUPSummery> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }
    public class AOGFollowUPQueryModel : AOGFollowUPSummery
    {
        public List<RemarkSummery> Remarks { get; set; } = new List<RemarkSummery>();
    }
}
