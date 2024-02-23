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

        public void Delete(Guid id)
        {
            _context.Remove(_context.Quotations.FindAsync(id).Result);
        }

        public async Task<List<Domain.Quotation.Quotation>> GetAllQuotations()
        {
            return await _context.Quotations.ToListAsync();
        }

        public async Task<Domain.Quotation.Quotation> GetQuotationByIdAsync(Guid id)
        {
            var quotation = await _context.Quotations.FindAsync(id);
            if (quotation != null)
            {
                await _context.Entry(quotation).Collection(x => x.QuotationPartsLists).LoadAsync();
            }
            return quotation;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Domain.Quotation.Quotation quotation)
        {
            _context.Entry(quotation).State = EntityState.Modified;
        }
    }
}
