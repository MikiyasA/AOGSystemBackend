using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IAOGFollowUpRepository
    {
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
        AOGFollowUp Add(AOGFollowUp AOGFollowUp);
        void Update(AOGFollowUp AOGFollowUp);
        void Delete(int id);
        Task<List<AOGFollowUp>> GetAllAOGFollowUpAsync();
        Task<List<AOGFollowUp>> GetAllActiveFollowUpAsync();
        Task<AOGFollowUp> GetAOGFollowUpByIDAsync(int id);

    }
}
