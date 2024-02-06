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
        Task<Company> GetCompanyByIDAsync(int? id);
        List<Company> GetCompanyByCode(string code);
        Company GetSingleCompanyByCode(string code);
        List<Company> GetCompanyByName(string name);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);

    }
}
