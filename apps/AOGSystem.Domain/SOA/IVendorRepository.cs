using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.SOA
{
    public interface  IVendorRepository
    {
        Vendor Add(Vendor vendor);
        void Update(Vendor vendor);
        void Delete(Guid id);
        Task<PaginatedList<Vendor>> GetAllVendorSOA(Expression<Func<Vendor, bool>> predicate, int page, int pageSize);
        Task<Vendor> GetVendorSOAByIDAsync(Guid? id);
        Task<Vendor> GetVendorSOAByCodeAsync(string code);
        Task<Vendor> GetVendorSOAByNameAsync(string vendorName);
        Task<List<Vendor>> GetAllActiveVendorSOAAsync();

        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
    }
}
