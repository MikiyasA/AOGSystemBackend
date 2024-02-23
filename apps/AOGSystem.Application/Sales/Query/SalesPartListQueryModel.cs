using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Sales.Query
{
    public class SalesPartListQueryModel
    {
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
        public string? RID { get; set; }
        public string? SerialNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
