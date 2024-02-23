using AOGSystem.Domain.FollowUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Invoices
{
    public interface IInvoiceRepository
    {
        Invoice Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(Guid id);
        Task<PaginatedList<Invoice>> GetAllInvoices(Expression<Func<Invoice, bool>> predicate, int page, int pageSize);
        Task<Invoice> GetInvoiceByIDAsync(Guid id);
        Task<List<Invoice>> GetInvoiceBySalesOrderIdAsync(Guid orderId);
        Task<List<Invoice>> GetInvoiceByLoanOrderIdAsync(Guid orderId);
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
