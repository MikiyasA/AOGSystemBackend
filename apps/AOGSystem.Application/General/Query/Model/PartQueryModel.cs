using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class PartQuerySummary
    {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string StockNo { get; set; }
        public string FinancialClass { get; set; }

        internal static Part ToModel(PartQuerySummary item)
        {
            return new Part(
                item.PartNumber,
                item.Description,
                item.StockNo,
                item.FinancialClass);
        }
        internal static List<Part> ToModel(List<PartQuerySummary> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();

        }
    }
    public class PartQueryModel : PartQuerySummary
    {
    }
}
