using AOGSystem.Domain;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.SOA;
using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class InvoiceListRepository : IInvoiceListRepository
    {
        private readonly AOGSystemContext _context;
        public InvoiceListRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public InvoiceList Add(InvoiceList invoiceList)
        {
            return _context.InvoiceLists.Add(invoiceList).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.InvoiceLists.FindAsync(id).Result);
        }

        public async Task<List<InvoiceList>> GetAllActiveSOAInvoiceListAsync()
        {
            return await _context.InvoiceLists.Where(x => x.Status.ToLower() != "closed")
                .Include(x => x.BuyerRemarks)
                .Include(x => x.FinanceRemarks)
                .ToListAsync();
        }

        public async Task<PaginatedList<InvoiceList>> GetAllSOAInvoiceList(Expression<Func<InvoiceList, bool>> predicate, int page, int pageSize)
        {
            IQueryable<InvoiceList> query = _context.InvoiceLists;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            var result = await PaginatedList<InvoiceList>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
        }

        public async Task<InvoiceList> GetSOAInvoiceListByIDAsync(Guid? id)
        {
            var invoiceList = await _context.InvoiceLists.FirstOrDefaultAsync(x => x.Id == id);

            if (invoiceList != null)
            {
                await _context.Entry(invoiceList)
                    .Collection(x => x.BuyerRemarks)
                    .LoadAsync();

                await _context.Entry(invoiceList)
                    .Collection(x => x.FinanceRemarks)
                    .LoadAsync();
            }

            return invoiceList;
        }

        public async Task<InvoiceList> GetSOAInvoiceListByInvoiceNoAsync(string invoiceNo, Guid vendorId)
        {
            var invoiceList = await _context.InvoiceLists
                  .Include(x => x.BuyerRemarks)
                  .Include(x => x.FinanceRemarks)
                  .FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo && x.VendorId == vendorId);

            return invoiceList;
        }

        public async Task<List<InvoiceList>> GetSOAInvoiceListByOrderNoAsync(string poNo)
        {
            return await _context.InvoiceLists.Where(x => x.PONo == poNo)
                .Include(x => x.BuyerRemarks)
                .Include(x => x.FinanceRemarks)
                .ToListAsync();
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(InvoiceList invoiceList)
        {
            _context.Entry(invoiceList).State = EntityState.Modified;
        }
    }
}
