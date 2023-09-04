using AOGSystem.Application.FollowUp.HomeBase.Query.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.HomeBase.Query
{
    public interface IHomeBaseFPQuery
    {
        Task<HomeBaseFollowUPQueryModel> GetHomeBaseFollowUpByIdAsync(int id);
    }
}
