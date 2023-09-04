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
        Task<Company> GetCompanyAsync(int id);
    }
}
