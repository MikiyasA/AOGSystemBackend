using AOGSystem.Domain.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.General
{
    public class PartRepository : IPartRepository
    {
        private readonly AOGSystemContext _context;
        public PartRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Part Add(Part part)
        {
            return _context.Parts.Add(part).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Parts.FindAsync(id).Result);
        }

        public Task<List<Part>> GetAllParts()
        {
            return _context.Parts.ToListAsync();
        }

        public async Task<Part> GetPartByIDAsync(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            if(part != null)
            {
                _context.Entry(part);
            }
            return part;
        }

        public async Task<Part> GetPartByPNAsync(string partNo)
        {
            var part =  await _context.Parts.FirstOrDefaultAsync(x => x.PartNumber == partNo);
            if(part != null)
            {
                _context.Entry(part);
            }
            return part;
        }

        public List<Part> GetPartByPartialPN(string partNo)
        {
            var parts = _context.Parts.Where(x => x.PartNumber.Contains(partNo)).ToList();
            return parts;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);

        }

        public void Update(Part part)
        {
            _context.Entry(part).State = EntityState.Modified;
        }

        public List<Part> GetPartByManufacturer(string manufacturer)
        {
            var parts = _context.Parts.Where(x => x.Manufacturer.Contains(manufacturer)).ToList();
            return parts;
        }

        public List<Part> GetPartByType(string type)
        {
            var parts = _context.Parts.Where(x => x.PartType.Contains(type)).ToList();
            return parts;
        }
    }
}
