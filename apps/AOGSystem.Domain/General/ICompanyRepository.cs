using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public interface ICompanyRepository
    {
        Company Add(Company company);
        void Update(Company company);
        void Delete(int id);
        Task<List<Company>> GetAllCompanyAsync();
        Task<Company> GetCompanyByIDAsync(int id);
        Task<Company> GetCompanyByCodeAsync(string code);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);

    }
}
