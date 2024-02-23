using AOGSystem.Domain.FollowUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public interface ILoanRepository
    {
        Loan Add(Loan loan);
        void Update(Loan loan);
        void Delete(int id);

        Task<PaginatedList<Loan>> GetAllLoans(Expression<Func<Loan, bool>> predicate, int page, int pageSize);
        Task<Loan> GetLoanByIDAsync(Guid? id);
        Task<List<Loan>> GetLoanByCompanyIdAsync(Guid companyId);
        Task<Loan> GetLoanByOrderNoAsync(string orderNo);
        Task<Loan> GetLoanByCustomerOrderNoAsync(string customerOrderNo);
        Task<List<Loan>> GetAllActiveAsync();
        Task<Loan> GetLastLoanOrder();
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
