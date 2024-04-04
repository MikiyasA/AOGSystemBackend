using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.SOA
{
    public interface IInvoiceListRepository
    {

        InvoiceList Add(InvoiceList invoiceList);
        void Update(InvoiceList invoiceList);
        void Delete(Guid id);
        Task<PaginatedList<InvoiceList>> GetAllSOAInvoiceList(Expression<Func<InvoiceList, bool>> predicate, int page, int pageSize);
        Task<InvoiceList> GetSOAInvoiceListByIDAsync(Guid? id);
        Task<List<InvoiceList>> GetSOAInvoiceListByOrderNoAsync(string poNo);
        Task<InvoiceList> GetSOAInvoiceListByInvoiceNoAsync(string invoiceNo, Guid vendorId);
        Task<List<InvoiceList>> GetAllActiveSOAInvoiceListAsync();

        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }

}
