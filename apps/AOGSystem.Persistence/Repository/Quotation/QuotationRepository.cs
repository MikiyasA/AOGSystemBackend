using AOGSystem.Domain.Quotation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Quotation
{
    public class QuotationRepository : IQuotationRepository
    {
        private readonly AOGSystemContext _context;
        public QuotationRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Domain.Quotation.Quotation Add(Domain.Quotation.Quotation quotation)
        {
            return _context.Quotations.Add(quotation).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Quotations.FindAsync(id).Result);
        }

        public async Task<Domain.Quotation.Quotation> GetQuotationAsync(int id)
        {
            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation != null)
            {
                _context.Entry(quotation);
            }
            return quotation;
        }

        public void Update(Domain.Quotation.Quotation quotation)
        {
            _context.Entry(quotation).State = EntityState.Modified;
        }
    }
}
