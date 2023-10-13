using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.CoreFollowUps
{
    public interface ICoreFollowUpRepository
    {
        CoreFollowUp Add(CoreFollowUp coreFollowUp);
        void Update(CoreFollowUp coreFollowUp);
        void Delete(int id);
        Task<List<CoreFollowUp>> GetAllCoreFollowUps();
        Task<CoreFollowUp> GetCoreFollowUpByIDAsync(int id);
        Task<CoreFollowUp> GetCoreFollowUpByPONoAsync(string pONo);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}