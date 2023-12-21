using AOGSystem.Domain.FollowUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class ActiveAOGFollowupDTO
    {
        public int Id { get; set; }
        public string? RID { get; set; }
        public DateTime RequestDate { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? WorkLocation { get; set; }
        public string? AOGStation { get; set; }

        public string? Customer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? PONumber { get; set; }
        public string? OrderType { get; set; }
        public int Quantity { get; set; }
        public string? UOM { get; set; }
        public string? Vendor { get; set; }
        public DateTime? EDD { get; set; } 
        public string? Status { get; set; }
        public string? AWBNo { get; set; }
        public bool NeedHigherMgntAttn { get; set; }
        public IReadOnlyCollection<Remark> Remarks { get; set; }
    }

    public class ActiveFollowUpTabsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }
        public IReadOnlyCollection<ActiveAOGFollowupDTO> FollowUps { get; set; }

    }
}
