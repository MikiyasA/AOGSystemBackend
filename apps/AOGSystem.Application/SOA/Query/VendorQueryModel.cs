using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Query
{
    public class VendorQueryModel
    {
        public Guid Id { get; set; }
        public string VendorName { get; set; }
        public string VendorCode { get; set; }
        public string Address { get; set; }
        public string? VendorAccountManagerName { get; set; }
        public string? VendorAccountManagerEmail { get; set; }
        public string? VendorFinanceContactName { get; set; }
        public string? VendorFinanceContactEmail { get; set; }
        public double? CreditLimit { get; set; }
        public double? TotalOutstanding { get; set; }
        public double? UnderProcess { get; set; }
        public double? UnderDispute { get; set; }
        public double? PaidAmount { get; set; }
        public string? ETFinanceContactName { get; set; }
        public string? ETFinanceContactEmail { get; set; }
        public DateTime? CertificateExpiryDate { get; set; }
        public DateTime? AssessmentDate { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }
    }
}
