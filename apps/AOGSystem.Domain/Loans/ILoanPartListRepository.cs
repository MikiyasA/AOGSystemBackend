using AOGSystem.Domain.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public interface ILoanPartListRepository
    {
        LoanPartList Add(LoanPartList loanPartList);
        void Update(LoanPartList loanPartList);
        void Delete(Guid id);
        Task<List<LoanPartList>> GetAllLoanPartLists();
        Task<LoanPartList> GetLoanPartListByIDAsync(Guid id);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
