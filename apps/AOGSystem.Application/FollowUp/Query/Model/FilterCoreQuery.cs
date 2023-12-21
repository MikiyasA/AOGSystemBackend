using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class FilterCoreQuery
    {
        public DateTime CreatedStartDate { get; set; }
        public DateTime CreatedEndDate { get; set; }
        public DateTime ReturnStartDate { get; set; }
        public DateTime ReturnEndDate { get; set; }
        public DateTime PODStartDate { get; set; }
        public DateTime PODEndDate { get; set; }
        public string? Status { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? PN { get; set; }
        public string? Vendor { get; set; }
        public string? AWBNo { get; set; }
        public string? ReturnPart { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
