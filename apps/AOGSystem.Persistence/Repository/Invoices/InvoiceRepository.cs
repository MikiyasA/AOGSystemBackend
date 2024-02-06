using AOGSystem.Domain.Invoices;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AOGSystemContext _context;
        public InvoiceRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Invoice Add(Invoice invoice)
        {
            return _context.Invoices.Add(invoice).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Invoices.FindAsync(id).Result);
        }

        public async Task<List<Invoice>> GetActiveInvoices()
        {
            return await _context.Invoices.Where(x => x.Status.ToLower() != "close").Include(x => x.InvoicePartLists).ToListAsync();
        }

        public Task<List<Invoice>> GetAllInvoices()
        {
            return _context.Invoices.ToListAsync();
        }

        public async Task<List<Invoice>> GetApprovedlInvoices()
        {
            return await _context.Invoices.Where(x => x.IsApproved == true).ToListAsync();
        }

        public async Task<Invoice> GetInvoiceByIDAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                await _context.Entry(invoice)
                    .Collection(x => x.InvoicePartLists).LoadAsync();
            }
            return invoice;
        }

        public async Task<Invoice> GetInvoiceByInvoiceNoAsync(string invoiceNo)
        {
            var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
            if (invoice != null)
            {
                _context.Entry(invoice);
            }
            return invoice;
        }

        public async Task<List<Invoice>> GetInvoiceByInvoicesNoAsync(string invoiceNo)
        {
            return await _context.Invoices.Where(x => x.InvoiceNo.Contains(invoiceNo))
                .Include(x => x.InvoicePartLists)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoiceByLoanOrderIdAsync(int orderId)
        {
            return await _context.Invoices.Where(x => x.LoanOrderId == orderId)
                .Include(x => x.InvoicePartLists)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoiceBySalesOrderIdAsync(int orderId)
        {
            return await _context.Invoices.Where(x => x.SalesOrderId == orderId)
                .Include(x => x.InvoicePartLists)
                .ToListAsync();
            
        }

        public async Task<List<Invoice>> GetInvoicesBytransactionType(string type)
        {
            return await _context.Invoices.Where(x => x.TransactionType == type).ToListAsync();
        }

        public async Task<Invoice> GetLastInvoice()
        {
            return await _context.Invoices.OrderByDescending(x => x.InvoiceNo).FirstOrDefaultAsync(); ;
        }

        public async Task<List<Invoice>> GetUnapprovedlInvoices()
        {
            return await _context.Invoices.Where(x => x.IsApproved == false).ToListAsync();
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Invoice invoice)
        {
            _context.Entry(invoice).State = EntityState.Modified;
        }
    }
}
