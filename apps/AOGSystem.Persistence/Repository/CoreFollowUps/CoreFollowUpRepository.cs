using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.General;
using MassTransit.Internals.GraphValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AOGSystem.Persistence.Repository.CoreFollowUps
{
    public class CoreFollowUpRepository : ICoreFollowUpRepository
    {
        private readonly AOGSystemContext _context;
        public CoreFollowUpRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CoreFollowUp Add(CoreFollowUp coreFollowUp)
        {
            return _context.CoreFollowUps.Add(coreFollowUp).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.CoreFollowUps.FindAsync(id).Result);
        }

        public async Task<List<CoreFollowUp>> GetActiveCoreFollowUps()
        {
            var core = await _context.CoreFollowUps
                .Where(x => x.Status != "Closed")
                .OrderBy(x => x.ReturnDueDate)
                .ToListAsync();
            return core;
        }

        public async Task<List<CoreFollowUp>> GetAllCoreFollowUps(int pageN, int pageS)
        {
            var pageNo = pageN != 0 ? pageN : 1;
            var pageSize = pageS != 0 ? pageS : 10;

            var coreFp = await _context.CoreFollowUps
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return coreFp;
        }

        public async Task<CoreFollowUp> GetCoreFollowUpByIDAsync(int id)
        {
            var coreFP = await _context.CoreFollowUps.FindAsync(id);
            if (coreFP != null)
            {
                _context.Entry(coreFP);
            }
            return coreFP;
        }

        public async Task<CoreFollowUp> GetCoreFollowUpByPONoAsync(string pONo)
        {
            var coreFP = await _context.CoreFollowUps.FirstOrDefaultAsync(x => x.PONo == pONo);
            if (coreFP != null)
            {
                _context.Entry(coreFP);
            }
            return coreFP;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(CoreFollowUp coreFollowUp)
        {
            _context.Entry(coreFollowUp).State = EntityState.Modified;
        }
    }
}
