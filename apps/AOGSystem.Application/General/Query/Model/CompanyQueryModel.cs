using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class CompanyQuerySummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string ShipToAddress { get; set; }
        public string BillToAddress { get; set; }
        public string PaymentTerm { get; set; }

        internal static Company ToModel(CompanyQuerySummary item)
        {
            return new Company(
                item.Name,
                item.Code,
                item.Address,
                item.City,
                item.Country,
                item.Phone,
                item.ShipToAddress,
                item.BillToAddress,
                item.PaymentTerm);

        }

        internal static List<Company> ToModel(List<CompanyQuerySummary> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }

    public class CompanyQueryModel : CompanyQuerySummary
    {
    }
}
