using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class PartQueryModel
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string StockNo { get; set; }
        public string FinancialClass { get; set; }
        public string Manufacturer { get; set; }
        public string PartType { get; set; }

        
    }
}
