using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class FollowupSearchQuery
    {
        public string? RID { get; set; }
        public DateTime? RequestDateFrom { get; set; }
        public DateTime? RequestDateTo { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? WorkLocation { get; set; }
        public string? AOGStation { get; set; }
        public string? Customer { get; set; }
        public string? PONumber { get; set; } 
        public string? OrderType { get; set; }
        public string? Vendor { get; set; }
        public string? Status { get; set; }
        public string? AWBNo { get; set; }
        public string? FlightNo { get; set; }
        public bool? NeedHigherMgntAttn { get; set; }
        public Guid? PartId { get; set; }
        public Guid? FollowUpTabsId { get; set; }
    }
}
