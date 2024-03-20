using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.CoreFollowUps
{
    public interface ICoreFollowUpRepository
    {
        CoreFollowUp Add(CoreFollowUp coreFollowUp);
        void Update(CoreFollowUp coreFollowUp);
        void Delete(Guid id);
        Task<PaginatedList<CoreFollowUp>> GetAllCoreFollowUps(Expression<Func<CoreFollowUp, bool>> predicate, int page, int pageSize);
        Task<List<CoreFollowUp>> GetActiveCoreFollowUps();
        Task<CoreFollowUp> GetCoreFollowUpByIDAsync(Guid id);
        Task<CoreFollowUp> GetCoreFollowUpByPONoAsync(string pONo);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}