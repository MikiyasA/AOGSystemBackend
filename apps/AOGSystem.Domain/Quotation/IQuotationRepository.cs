using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Quotation
{
    public interface IQuotationRepository
    {
        Quotation Add(Quotation quotation);
        void Update(Quotation quotation);
        void Delete(int id);
        Task<Quotation> GetQuotationAync(int id);
    }
}
