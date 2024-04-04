using AOGSystem.Domain;
using AOGSystem.Domain.Sales;
using AOGSystem.Domain.SOA;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
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
            var vendors =  await _context.Vendors.Where(x => x.Status.ToLower() != "closed")
                .Include(x => x.InvoiceLists)
                .OrderByDescending(x => x.TotalOutstanding)
                .ToListAsync();
            foreach (var vendor in vendors)
            {
                vendor.UpdateFinancialData();
            }
            return vendors;
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
            var vendor = await _context.Vendors.FirstOrDefaultAsync(x => x.VendorCode == code);
            if (vendor != null)
            {
                await _context.Entry(vendor)
                    .Collection(x => x.InvoiceLists).LoadAsync();
                vendor.UpdateFinancialData();
            }
            return vendor;
        }

        public async Task<Vendor> GetActiveVendorSOAByIDAsync(Guid? id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                await _context.Entry(vendor)
                    .Collection(x => x.InvoiceLists)
                    .Query()
                    .Where(x => x.Status != "closed")
                    .Include(x => x.BuyerRemarks.OrderByDescending(x => x.CreatedAT))
                    .Include(x => x.FinanceRemarks.OrderByDescending(x => x.CreatedAT))
                    .OrderBy(x => x.DueDate)
                    .LoadAsync();
               
                vendor.UpdateFinancialData();
            }
            return vendor; 
        }

        public async Task<PaginatedList<InvoiceList>> GetAllVendorSOAByIDAsync(Guid? vendorId, Expression<Func<InvoiceList, bool>> predicate, int page, int pageSize)
        {
            var vendor = await _context.Vendors.FindAsync(vendorId);
            if (vendor == null)
            {
                return null;
            }

            var invoiceListsQuery = _context.InvoiceLists
                .Where(x => x.VendorId == vendorId)
                .Where(predicate)
                .Include(x => x.BuyerRemarks)
                .Include(x => x.FinanceRemarks)
                .OrderByDescending(x => x.CreatedAT);

            var pagedInvoiceLists = await PaginatedList<InvoiceList>.ToPagedList(invoiceListsQuery, page, pageSize);

            return pagedInvoiceLists;
        }

        public async Task<List<Vendor>> GetVendorSOAByNameAsync(string vendorName)
        {
            var vendors = await _context.Vendors.Where(x => x.VendorName.Contains(vendorName))
                .Include(x => x.InvoiceLists)
                .ToListAsync();
            foreach (var vendor in vendors)
            {
                vendor.UpdateFinancialData();
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

        public async Task<List<Vendor>> GetActiveVendorSOAByUserIdAsync(Guid? userId, string userFullName)
        {
            var vendor = await _context.Vendors.Where(x => x.Status != "Closed" &&
                            (userId == null || x.SOAHandlerBuyerId == userId || x.ETFinanceContactName.Contains(userFullName))).ToListAsync();
            return vendor;
        }
    }
}
