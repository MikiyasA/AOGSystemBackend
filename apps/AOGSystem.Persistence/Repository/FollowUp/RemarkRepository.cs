using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.FollowUp
{
    public class RemarkRepository : IRemarkRepository
    {

        private readonly AOGSystemContext _context;
        public RemarkRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Remark Add(Remark remark)
        {
            return _context.Remarks.Add(remark).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Remarks.FindAsync(id).Result);
        }

        public async Task<Remark> GetRemarkAsync(int id)
        {
            var remark = await _context.Remarks.FindAsync(id);
            if(remark != null)
            {
                _context.Entry(remark);
            }
            return remark;
        }

        public void Update(Remark remark)
        {
            _context.Entry(remark).State = EntityState.Modified;
        }
    }
}
