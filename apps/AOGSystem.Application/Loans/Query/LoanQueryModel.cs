using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Query
{
    public class LoanQueryModel
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public Guid CompanyId { get; set; }
        public string CustomerOrderNo { get; set; }
        public string OrderedByName { get; set; }
        public string? OrderedByEmail { get; set; }
        public string? Status { get; set; }
        public bool IsApproved { get; set; }
        public string? Note { get; set; }
    }
}
