using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace AOGSystem.Domain.FollowUp
{
    public interface IAOGFollowUpRepository
    {
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
        AOGFollowUp Add(AOGFollowUp AOGFollowUp);
        void Update(AOGFollowUp AOGFollowUp);
        void Delete(Guid id);
        Task<PaginatedList<AOGFollowUp>> GetAllAOGFollowUpAsync(Expression<Func<AOGFollowUp, bool>> predicate, int page, int pageSize);
        Task<List<AOGFollowUp>> GetAllActiveFollowUpAsync();
        Task<List<AOGFollowUp>> GetAllActiveFollowUpByTabIdAsync(Guid id);
        Task<AOGFollowUp> GetAOGFollowUpByIDAsync(Guid id);

    }
}
