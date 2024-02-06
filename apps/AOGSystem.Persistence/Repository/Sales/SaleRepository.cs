using AOGSystem.Domain.General;
using AOGSystem.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Ocelot.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Sales
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AOGSystemContext _context;
        public SaleRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Domain.Sales.Sales Add(Domain.Sales.Sales sales)
        {
            return _context.Sales.Add(sales).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Sales.FindAsync(id).Result);
        }

        public async Task<List<Domain.Sales.Sales>> GetAllActiveAsync()
        {
            return await _context.Sales.Where(x => x.Status.ToLower() != "closed")
                .Include(x => x.SalesPartLists)
                .ToListAsync();

        }

        public async Task<List<Domain.Sales.Sales>> GetAllSales()
        {
            return await _context.Sales.ToListAsync();
        }

        public async Task<Domain.Sales.Sales> GetLastSalesOrder()
        {
            return await _context.Sales.OrderByDescending(x => x.OrderNo).FirstOrDefaultAsync(); ;
        }

        public async Task<List<Domain.Sales.Sales>> GetSalesByCompanyIdAsync(int companyId)
        {
            return await _context.Sales.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        public async Task<Domain.Sales.Sales> GetSalesByCustomerOrderNoAsync(string customerOrderNo)
        {
            var sales = await _context.Sales.FirstOrDefaultAsync(x => x.CustomerOrderNo == customerOrderNo);
            if (sales != null)
            {
                _context.Entry(sales);
            }
            return sales;
        }

        public async Task<Domain.Sales.Sales> GetSalesByIDAsync(int? id)
        {
            var sales = await _context.Sales.FindAsync(id);
            if (sales != null)
            {
                await _context.Entry(sales)
                    .Collection(x => x.SalesPartLists).LoadAsync();
            }
            return sales;
        }

        public async Task<Domain.Sales.Sales> GetSalesByOrderNoAsync(string orderNo)
        {
            var sales = await _context.Sales.FirstOrDefaultAsync(x => x.OrderNo == orderNo);
            if (sales != null)
            {
                await _context.Entry(sales)
                    .Collection(x => x.SalesPartLists).LoadAsync(); ;
            }
            return sales;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Domain.Sales.Sales sales)
        {
            _context.Entry(sales).State = EntityState.Modified;
        }
    }
}
