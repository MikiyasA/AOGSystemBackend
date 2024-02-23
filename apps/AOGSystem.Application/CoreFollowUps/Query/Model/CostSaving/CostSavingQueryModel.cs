using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Query.Model.CostSaving
{
    public class CostSavingQueryModel
    {
        public Guid Id { get; set; }
        public string? OldPO { get; set; }
        public string NewPO { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? CNDate { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? PriceVariance { get; set; }
        public int? Quantity { get; set; }
        public decimal? SavingInUSD { get; set; }
        public decimal? SavingInETB { get; set; }
        public string? Remark { get; set; }
        public bool? IsPurchaseOrder { get; set; }
        public bool? IsRepairOrder { get; set; }
        public string? SavedBy { get; set; }
        public string? Status { get; set; }
    }
}
