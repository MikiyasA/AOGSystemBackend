﻿using AOGSystem.Domain.General;
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

        public void Delete(int id)
        {
            _context.Remove(_context.Companies.FindAsync(id).Result);
        }

        public Task<List<Company>> GetAllCompanyAsync()
        {
            return _context.Companies.ToListAsync();
        }

        public async Task<Company> GetCompanyByCodeAsync(string code)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Code == code);
            if (company != null)
            {
                _context.Entry(company);
            }
            return company;
        }

        public async Task<Company> GetCompanyByIDAsync(int id)
        {
            var company = await _context.Companies.FindAsync(id);
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