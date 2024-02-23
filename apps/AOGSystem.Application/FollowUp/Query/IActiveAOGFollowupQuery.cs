using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.General.Query.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query
{
    public interface IActiveAOGFollowupQuery
    {
        Task<List<ActiveAOGFollowupDTO>> GetAllActiveFollowUpAsync();
        Task<List<ActiveAOGFollowupDTO>> GetAllActiveFollowUpByTabIdAsync(Guid id);
        Task<List<ActiveFollowUpTabsDto>> GetAllActiveFollowUpTabsAsync();
    }
}
