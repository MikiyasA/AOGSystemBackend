using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Query
{
    public class RemarkQueryModel
    {
        public Guid InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string Message { get; set; }
    }
}
