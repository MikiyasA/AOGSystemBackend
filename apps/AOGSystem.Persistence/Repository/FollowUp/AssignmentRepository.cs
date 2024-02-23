using AOGSystem.Domain;
using AOGSystem.Domain.FollowUp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<PaginatedList<Assignment>> GetAllAssignment(Expression<Func<Assignment, bool>> predicate, int page, int pageSize)
        {
            //return _context.Assignments.ToListAsync();
            IQueryable<Assignment> query =  _context.Assignments;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var result = await PaginatedList<Assignment>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
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

        public async Task<List<Assignment>> GetActiveAssignmentByUserId(Guid? userId)
        {
            var assignments = await _context.Assignments
                .Where(x => x.Status != "Closed" &&
                            (userId == null || x.AssignedTo == userId || x.ReAssignedTo == userId))
                .ToListAsync();
            return assignments;
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
