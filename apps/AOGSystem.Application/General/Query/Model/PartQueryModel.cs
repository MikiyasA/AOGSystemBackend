using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class PartQuerySummer
    {
        public int Id { get; set; }
        public string PartNumber { get; private set; }
        public string Description { get; private set; }
        public string StockNo { get; private set; }
        public string FinancialClass { get; private set; }

        internal static Part ToModel(PartQuerySummer item)
        {
            return new Part(
                item.PartNumber,
                item.Description,
                item.StockNo,
                item.FinancialClass);
        }
        internal static List<Part> ToModel(List<PartQuerySummer> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();

        }
    }
    public class PartQueryModel : PartQuerySummer
    {
    }
}
