using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public interface ISaleRepository
    {
        Sales Add(Sales sales);
        void Update(Sales sales);
        void Delete(int id);
        Task<List<Sales>> GetAllSales();
        Task<Sales> GetSalesByIDAsync(int? id);
        Task<List<Sales>> GetSalesByCompanyIdAsync(int companyId);
        Task<Sales> GetSalesByOrderNoAsync(string orderNo);
        Task<Sales> GetSalesByCustomerOrderNoAsync(string customerOrderNo);
        Task<List<Sales>> GetAllActiveAsync();
        Task<Sales> GetLastSalesOrder();


        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
