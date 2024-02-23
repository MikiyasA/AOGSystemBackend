using AOGSystem.Domain.General;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.General
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AOGSystemContext _context;
        public CompanyRepository(AOGSystemContext context)
        {
            _context= context ?? throw new ArgumentNullException(nameof(context));
        }
        public  Company Add(Company company)
        {
            return  _context.Companies.Add(company).Entity;
        }

        public void Delete(Guid id)
        {
            _context.Remove(_context.Companies.FindAsync(id).Result);
        }

        public Task<List<Company>> GetAllCompanyAsync()
        {
            return _context.Companies.ToListAsync();
        }

        public List<Company> GetCompanyByCode(string code)
        {
            return  _context.Companies.Where(x => x.Code.Contains(code)).ToList();

        }

        public async Task<Company> GetCompanyByIDAsync(Guid? id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Entry(company);
            }
            return company;
        }

        public List<Company> GetCompanyByName(string name)
        {
            return _context.Companies.Where(x => x.Name.Contains(name)).ToList();
        }

        public  Company GetSingleCompanyByCode(string code)
        {
            var company =  _context.Companies.FirstOrDefault(x => x.Code == code);
            if (company != null)
            {
                _context.Entry(company);
            }
            return company;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Company company)
        {
            _context.Entry(company).State = EntityState.Modified;
        }
    }
}
