using AOGSystem.Domain.CoreFollowUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.CostSavings
{
    public interface ICostSavingRepository
    {
        CostSaving Add(CostSaving costSaving);
        void Update(CostSaving costSaving);
        void Delete(int id);
        Task<PaginatedList<CostSaving>> GetAllCostSavings(Expression<Func<CostSaving, bool>> predicate, int page, int pageSize);
        Task<List<CostSaving>> GetActiveCostSavings();
        Task<CostSaving> GetCostSavingByIDAsync(int id);
        Task<CostSaving> GetCostSavingByNewPONoAsync(string newPo);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
