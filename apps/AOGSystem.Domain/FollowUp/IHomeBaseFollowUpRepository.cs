using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IHomeBaseFollowUpRepository
    {
        HomeBaseFollowUp Add(HomeBaseFollowUp homeBaseFollowUp);
        void Update(HomeBaseFollowUp homeBaseFollowUp);
        void Delete(int id);
        Task<HomeBaseFollowUp> GetHomeBaseFollowUpAsync(int id);
    }
}
