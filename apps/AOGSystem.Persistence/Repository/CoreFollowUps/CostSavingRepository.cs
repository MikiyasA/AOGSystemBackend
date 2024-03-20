using AOGSystem.Domain;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.CostSavings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.CoreFollowUps
{
    public class CostSavingRepository : ICostSavingRepository
    {
        private readonly AOGSystemContext _context;
        public CostSavingRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public CostSaving Add(CostSaving costSaving)
        {
            return _context.CostSavings.Add(costSaving).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.CostSavings.FindAsync(id).Result);
        }

        public async Task<List<CostSaving>> GetActiveCostSavings()
        {
            var c = await _context.CostSavings
                .Where(x => x.Status != "Closed")
                .OrderByDescending(x => x.CreatedAT)
                .ToListAsync();
            return c;
        }

        public async Task<PaginatedList<CostSaving>> GetAllCostSavings(Expression<Func<CostSaving, bool>> predicate, int page, int pageSize)
        {
            IQueryable<CostSaving> query = _context.CostSavings;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            var result = await PaginatedList<CostSaving>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
        }

        public async Task<CostSaving> GetCostSavingByIDAsync(Guid id)
        {
            var c = await _context.CostSavings.FindAsync(id);
            if (c != null)
            {
                _context.Entry(c);
            }
            return c;
        }

        public async Task<CostSaving> GetCostSavingByNewPONoAsync(string newPo)
        {
            var c = await _context.CostSavings.FirstOrDefaultAsync(x => x.NewPO == newPo);
            if (c != null)
            {
                _context.Entry(c);
            }
            return c;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(CostSaving costSaving)
        {
            _context.Entry(costSaving).State = EntityState.Modified;
        }
    }
}
