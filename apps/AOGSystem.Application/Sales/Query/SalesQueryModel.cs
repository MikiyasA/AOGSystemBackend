using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Sales.Query
{
    public class SalesQueryModel
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public string? OrderByName { get; set; }
        public string? OrderByEmail { get; set; }
        public string OrderNo { get; set; }
        public string? CustomerOrderNo { get; set; }
        public string? ShipToAddress { get; set; }
        public string Status { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
        public bool IsFullyShipped { get; set; }
        public string? AWBNo { get; set; }
        public DateTime? ShipDate { get; set; }
        public bool ReceivedByCustomer { get; set; }
        public DateTime? ReceivedDate { get; set; }
    }
}
