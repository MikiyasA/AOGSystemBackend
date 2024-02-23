using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Query
{
    public class QuotationPartListSummary
    {
        public Guid Id { get; set; }
        public Guid PartId { get;  set; }
        public Guid QuotationId { get;  set; }
        public decimal CurrentPrice { get;  set; }
        public decimal SalesPrice { get;  set; }
        public decimal FixedLoanPrice { get; set; }
        public decimal LoanPricePerDay { get; set; }
        public decimal ExchangePrice { get;  set; }
        public string? StockLocation { get;  set; }
        public string? Condition { get;  set; }
        public string? SerialNumber { get;  set; }

        internal static QuotationPartList ToModel(QuotationPartListSummary item)
        {
            return new QuotationPartList(item.PartId,
                item.CurrentPrice,
                item.SalesPrice,
                item.FixedLoanPrice,
                item.LoanPricePerDay,
                item.ExchangePrice,
                item.StockLocation,
                item.Condition,
                item.SerialNumber);
        }
        internal static List<QuotationPartList> ToMode(List<QuotationPartListSummary> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }
    public class QuotationPartListQueryModel : QuotationPartListSummary
    {
    }
}
