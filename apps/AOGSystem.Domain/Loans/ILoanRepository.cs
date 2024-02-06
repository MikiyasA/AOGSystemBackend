using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public interface ILoanRepository
    {
        Loan Add(Loan loan);
        void Update(Loan loan);
        void Delete(int id);

        Task<List<Loan>> GetAllLoans();
        Task<Loan> GetLoanByIDAsync(int? id);
        Task<List<Loan>> GetLoanByCompanyIdAsync(int companyId);
        Task<Loan> GetLoanByOrderNoAsync(string orderNo);
        Task<Loan> GetLoanByCustomerOrderNoAsync(string customerOrderNo);
        Task<List<Loan>> GetAllActiveAsync();
        Task<Loan> GetLastLoanOrder();
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
