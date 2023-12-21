using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public interface IAssignmentRepository
    {
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);
        Assignment Add(Assignment assignment);
        void Update(Assignment assignment);
        void Delete(int id);
        Task<List<Assignment>> GetAllAssignment();
        Task<List<Assignment>> GetActiveAssignment();
        Task<Assignment> GetAssignmentById(int id);
        Task<List<Assignment>> GetPersonalAssignment(int userId);
    }
}
