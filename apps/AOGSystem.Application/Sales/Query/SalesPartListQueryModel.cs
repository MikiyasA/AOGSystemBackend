using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Sales.Query
{
    public class SalesPartListQueryModel
    {
        public int Id { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public string Currency { get; set; }
        public string? RID { get; set; }
        public string? SerialNo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
