using AOGSystem.Domain;
using AOGSystem.Domain.Sales;
using AOGSystem.Domain.SOA;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.SOA
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AOGSystemContext _context;
        public VendorRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Vendor Add(Vendor vendor)
        {
            return _context.Vendors.Add(vendor).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.Vendors.FindAsync(id).Result);
        }

        public async Task<List<Vendor>> GetAllActiveVendorSOAAsync()
        {
            return await _context.Vendors.Where(x => x.Status.ToLower() != "closed")
                .Include(x => x.InvoiceLists)
                .ToListAsync();
        }

        public async Task<PaginatedList<Vendor>> GetAllVendorSOA(Expression<Func<Vendor, bool>> predicate, int page, int pageSize)
        {
            IQueryable<Vendor> query = _context.Vendors;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            var result = await PaginatedList<Vendor>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
        }

        public async Task<Vendor> GetVendorSOAByCodeAsync(string code)
        {
            var vendors = await _context.Vendors.FirstOrDefaultAsync(x => x.VendorCode == code);
            if (vendors != null)
            {
                await _context.Entry(vendors)
                    .Collection(x => x.InvoiceLists).LoadAsync(); ;
            }
            return vendors;
        }

        public async Task<Vendor> GetVendorSOAByIDAsync(Guid? id)
        {
            var vendors = await _context.Vendors.FindAsync(id);
            if (vendors != null)
            {
                await _context.Entry(vendors)
                    .Collection(x => x.InvoiceLists).LoadAsync();
            }
            return vendors; 
        }

        public async Task<Vendor> GetVendorSOAByNameAsync(string vendorName)
        {
            var vendors = await _context.Vendors.FirstOrDefaultAsync(x => x.VendorName == vendorName);
            if (vendors != null)
            {
                await _context.Entry(vendors)
                    .Collection(x => x.InvoiceLists).LoadAsync(); ;
            }
            return vendors;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Vendor vendor)
        {
            _context.Entry(vendor).State = EntityState.Modified;
        }
    }
}
