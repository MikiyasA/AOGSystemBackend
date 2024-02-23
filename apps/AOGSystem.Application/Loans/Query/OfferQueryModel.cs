using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Query
{
    public class OfferQueryModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }
    }
}

