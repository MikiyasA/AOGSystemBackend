using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Invoices
{
    public interface IInvoiceRepository
    {
        Invoice Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(int id);
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoiceByIDAsync(int id);
        Task<List<Invoice>> GetInvoiceBySalesOrderIdAsync(int orderId);
        Task<List<Invoice>> GetInvoiceByLoanOrderIdAsync(int orderId);
        Task<Invoice> GetInvoiceByInvoiceNoAsync(string invoiceNo);
        Task<List<Invoice>> GetInvoiceByInvoicesNoAsync(string invoiceNo);
        Task<List<Invoice>> GetApprovedlInvoices();
        Task<List<Invoice>> GetUnapprovedlInvoices();
        Task<List<Invoice>> GetActiveInvoices();
        Task<List<Invoice>> GetInvoicesBytransactionType(string type);
        Task<Invoice> GetLastInvoice();
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
