using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Loans
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AOGSystemContext _context;
        public LoanRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Loan Add(Loan loan)
        {
            return _context.Loans.Add(loan).Entity;

        }

        public void Delete(int id)
        {
            _context.Remove(_context.Loans.FindAsync(id).Result);
        }

        public async Task<List<Loan>> GetAllActiveAsync()
        {
            return await _context.Loans.Where(x => x.Status.ToLower() != "closed")
                .Include(x => x.LoanPartLists)
                .ThenInclude(x => x.Offers)
                .ToListAsync();
        }

        public async Task<List<Loan>> GetAllLoans()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<Loan> GetLastLoanOrder()
        {
            return await _context.Loans.OrderByDescending(x => x.OrderNo).FirstOrDefaultAsync();
        }

        public async Task<List<Loan>> GetLoanByCompanyIdAsync(int companyId)
        {
            return await _context.Loans.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Loan> GetLoanByCustomerOrderNoAsync(string customerOrderNo)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(x => x.CustomerOrderNo == customerOrderNo);
            if (loan != null)
            {
                _context.Entry(loan);
            }
            return loan;
        }

        public async Task<Loan> GetLoanByIDAsync(int? id)
        {
            var loan = await _context.Loans
                .Include(x => x.LoanPartLists)
                    .ThenInclude(lp => lp.Offers)
                .FirstOrDefaultAsync(x => x.Id == id);

            return loan;
        }

        public async Task<Loan> GetLoanByOrderNoAsync(string orderNo)
        {
            var loan = await _context.Loans
                .Include(x => x.LoanPartLists)
                    .ThenInclude(lp => lp.Offers)
                .FirstOrDefaultAsync(x => x.OrderNo == orderNo);

            return loan;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Loan loan)
        {
            _context.Entry(loan).State = EntityState.Modified;
        }
    }
}
