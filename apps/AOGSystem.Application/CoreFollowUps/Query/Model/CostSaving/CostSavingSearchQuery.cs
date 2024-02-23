using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Query.Model.CostSaving
{
    public class CostSavingSearchQuery
    {
        public string? OldPO { get; set; } 
        public string? NewPO { get; set; }
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public DateTime? CNDateFrom { get; set; }
        public DateTime? CNDateTo { get; set; }
        public bool? IsPurchaseOrder { get; set; }
        public bool? IsRepairOrder { get; set; }
        public string? SavedBy { get; set; }
        public string? Status { get; set; }
    }
}
