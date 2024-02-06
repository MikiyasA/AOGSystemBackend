using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public interface IOfferRepository
    {
        Offer Add(Offer offer);
        void Update(Offer offer);
        void Delete(int id);
        Task<List<Offer>> GetAllOffers();
        Task<Offer> GetOfferByIDAsync(int id);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
