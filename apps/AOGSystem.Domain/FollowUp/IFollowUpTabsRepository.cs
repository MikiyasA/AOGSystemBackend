using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IFollowUpTabsRepository
    {
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
        FollowUpTabs Add(FollowUpTabs AOGFollowUp);
        void Update(FollowUpTabs AOGFollowUp);
        void Delete(Guid id);
        Task<List<FollowUpTabs>> GetAllFollowUpTabsAsync();
        Task<List<FollowUpTabs>> GetAllActiveFollowUpTabsAsync();
        Task<FollowUpTabs> GetFollowUpTabsByIDAsync(Guid id);
        Task<FollowUpTabs> GetFollowUpTabsByNameAsync(string name);

    }
}
