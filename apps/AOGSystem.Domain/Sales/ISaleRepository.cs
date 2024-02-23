using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public interface ISaleRepository
    {
        Sales Add(Sales sales);
        void Update(Sales sales);
        void Delete(Guid id);
        Task<PaginatedList<Sales>> GetAllSales(Expression<Func<Sales, bool>> predicate, int page, int pageSize);
        Task<Sales> GetSalesByIDAsync(Guid? id);
        Task<List<Sales>> GetSalesByCompanyIdAsync(Guid companyId);
        Task<Sales> GetSalesByOrderNoAsync(string orderNo);
        Task<Sales> GetSalesByCustomerOrderNoAsync(string customerOrderNo);
        Task<List<Sales>> GetAllActiveAsync();
        Task<Sales> GetLastSalesOrder();


        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
