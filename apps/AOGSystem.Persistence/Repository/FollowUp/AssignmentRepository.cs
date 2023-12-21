using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.FollowUp
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly AOGSystemContext _context;
        public AssignmentRepository(AOGSystemContext context)
        {
            _context = context;
        }

        public Assignment Add(Assignment assignment)
        {
            return _context.Assignments.Add(assignment).Entity;
        }

        public void Delete(int id)
        {
            _context.Remove(_context.Assignments.FindAsync(id).Result);
        }

        public async Task<List<Assignment>> GetActiveAssignment()
        {
            return await _context.Assignments
                .OrderByDescending(x => x.DueDate)
                .Where(x => x.Status != "Closed").ToListAsync();
        }

        public Task<List<Assignment>> GetAllAssignment()
        {
            return _context.Assignments.ToListAsync();
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            var assignement = await _context.Assignments.FindAsync(id);
            if (assignement != null)
            {
                _context.Entry(assignement);
            }
            return assignement;
        }

        public Task<List<Assignment>> GetPersonalAssignment(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Assignment assignment)
        {
            _context.Entry(assignment).State = EntityState.Modified;
        }
    }
}
