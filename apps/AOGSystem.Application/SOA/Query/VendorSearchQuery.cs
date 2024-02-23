using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Query
{
    public class VendorSearchQuery
    {
        public string? VendorName { get; set; }
        public string? VendorCode { get; set; }
        public string? VendorAccountManagerName { get; set; }
        public string? VendorFinanceContactName { get; set; }
        public string? ETFinanceContactName { get; set; }
        public DateTime? CertificateExpiryDateFrom { get; set; }
        public DateTime? CertificateExpiryDateTo { get; set; }
        public string? Status { get; set; }
    }
}
