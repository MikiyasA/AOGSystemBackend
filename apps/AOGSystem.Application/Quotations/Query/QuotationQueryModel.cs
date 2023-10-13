using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Query
{
    public class QuotationSummary
    {
        public int Id { get; set; }
        public bool Loan { get;  set; }
        public bool Sales { get;  set; }
        public bool Exchange { get;  set; }
        public string CompanyCode { get; set; }
        public int CompanyId { get; set; }

        public string? RequestedByName { get;  set; }
        public string? RequestedByEmail { get;  set; }
        public string? RequestedByPhone { get;  set; }
        public User? OfferedBy { get;  set; }
        public int? OfferedById { get;  set; }

        public static Quotation ToModel(QuotationSummary item)
        {
            return new Quotation(
                item.Loan,
                item.Sales,
                item.Exchange,
                item.CompanyId,
                item.RequestedByName,
                item.RequestedByEmail,
                item.RequestedByPhone);
        }
        internal static List<Quotation> ToModel(List<QuotationSummary> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }
public class QuotationQueryModel : QuotationSummary
    {
        public List<QuotationPartListSummary> QuotationPartLists { get; set; } = new List<QuotationPartListSummary>();
    }
}
