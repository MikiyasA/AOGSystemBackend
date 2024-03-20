using AOGSystem.Domain.Loans;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Loans
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AOGSystemContext _context;
        public OfferRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Offer Add(Offer offer)
        {
            return _context.Offers.Add(offer).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.Offers.FindAsync(id).Result);
        }

        public async Task<List<Offer>> GetAllOffers()
        {
            return await _context.Offers.ToListAsync();
        }

        public async Task<Offer> GetOfferByIDAsync(Guid id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                 _context.Entry(offer);
            }
            return offer;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Offer offer)
        {
            _context.Entry(offer).State = EntityState.Modified;
        }
    }
}
