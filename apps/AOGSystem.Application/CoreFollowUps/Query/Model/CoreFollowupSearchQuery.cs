using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Query.Model
{
    public class CoreFollowupSearchQuery
    {
        public string? PONo { get; set; }
        public DateTime? POCreatedDateFrom { get; set; }
        public DateTime? POCreatedDateTo { get; set; }
        public string? Aircraft { get; set; }
        public string? TailNo { get; set; }
        public string? PartNumber { get; set; }
        public string? StockNo { get; set; }
        public string? Vendor { get; set; }
        public string? AWBNo { get; set; }
        public string? ReturnedPart { get; set; }
        public DateTime? PODDateFrom { get; set; }
        public DateTime? PODDateTo { get; set; }
        public string? Status { get; set; }
    }
}
