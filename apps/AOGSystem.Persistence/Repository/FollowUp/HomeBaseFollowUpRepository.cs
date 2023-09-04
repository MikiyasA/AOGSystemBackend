using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.FollowUp
{
    public class HomeBaseFollowUpRepository : IHomeBaseFollowUpRepository
    {
        private readonly AOGSystemContext _context;

        public HomeBaseFollowUpRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public HomeBaseFollowUp Add(HomeBaseFollowUp homeBaseFollowUp)
        {
            return _context.HomeBaseFollowUps.Add(homeBaseFollowUp).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.HomeBaseFollowUps.FindAsync(id).Result);
        }

        public async Task<HomeBaseFollowUp> GetHomeBaseFollowUpAsync(int id)
        {
            var followUp = await _context.HomeBaseFollowUps.FindAsync(id);
            if(followUp != null)
            {
                _context.Entry(followUp);
            }
            return followUp;
        }

        public void Update(HomeBaseFollowUp homeBaseFollowUp)
        {
            _context.Entry(homeBaseFollowUp).State = EntityState.Modified;
        }
    }
}
