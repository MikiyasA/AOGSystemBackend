using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Sales.Query
{
    public class SalesSearchQuery
    {
        public Guid? CompanyId { get; set; }
        public string? OrderByName { get; set; }
        public string? OrderByEmail { get; set; }
        public string? OrderNo { get; set; }
        public string? CustomerOrderNo { get; set; }
        public string? Status { get; set; }
        public string? AWBNo { get; set; }
        public DateTime? ShipDateFrom { get; set; }
        public DateTime? ShipDateTo { get; set; }
    }
}
