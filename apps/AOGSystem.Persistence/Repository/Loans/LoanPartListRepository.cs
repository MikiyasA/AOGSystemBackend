using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Loans
{
    public class LoanPartListRepository : ILoanPartListRepository
    {
        private readonly AOGSystemContext _context;
        public LoanPartListRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public LoanPartList Add(LoanPartList loanPartList)
        {
            return _context.LoanPartLists.Add(loanPartList).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.LoanPartLists.FindAsync(id).Result);
        }

        public async Task<List<LoanPartList>> GetAllLoanPartLists()
        {
            return await _context.LoanPartLists.ToListAsync();
        }

        public async Task<LoanPartList> GetLoanPartListByIDAsync(Guid id)
        {
            var loanPartList = await _context.LoanPartLists.FindAsync(id);
            if (loanPartList != null)
            {
                await _context.Entry(loanPartList)
                    .Collection(x => x.Offers).LoadAsync();
            }
            return loanPartList;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(LoanPartList loanPartList)
        {
            _context.Entry(loanPartList).State = EntityState.Modified;
        }
    }
}
