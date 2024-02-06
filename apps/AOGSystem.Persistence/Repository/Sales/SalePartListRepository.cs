using AOGSystem.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Sales
{
    public class SalePartListRepository : ISalePartListRepository
    {
        private readonly AOGSystemContext _context;
        public SalePartListRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public SalesPartList Add(SalesPartList salesPartList)
        {
            return _context.SalesPartLists.Add(salesPartList).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.SalesPartLists.FindAsync(id).Result);
        }

        public async Task<List<SalesPartList>> GetAllSalesPartLists()
        {
            return await _context.SalesPartLists.ToListAsync();
        }

        public async Task<SalesPartList> GetSalesPartListByIDAsync(int id)
        {
            var salesPartList = await _context.SalesPartLists.FindAsync(id);
            if (salesPartList != null)
            {
                _context.Entry(salesPartList);
            }
            return salesPartList;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(SalesPartList salesPartList)
        {
            _context.Entry(salesPartList).State = EntityState.Modified;
        }
    }
}
