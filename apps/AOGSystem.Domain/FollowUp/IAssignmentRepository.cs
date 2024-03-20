using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IAssignmentRepository
    {
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
        Assignment Add(Assignment assignment);
        void Update(Assignment assignment);
        void Delete(Guid id);
        Task<PaginatedList<Assignment>> GetAllAssignment(Expression<Func<Assignment, bool>> predicate, int page, int pageSize);
        Task<List<Assignment>> GetActiveAssignment();
        Task<Assignment> GetAssignmentById(Guid id);
        Task<List<Assignment>> GetActiveAssignmentByUserId(Guid? userId);
    }
}
