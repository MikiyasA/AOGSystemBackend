using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Assignments.Query.Model
{
    public class AssignmentSearchQuery
    {
        public string? Title { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public Guid? AssignedTo { get; set; }
        public Guid? StartBy { get; set; }
        public Guid? ReAssignedTo { get; set; }
        public Guid? ReAssignedBy { get; set; }
        public DateTime? FinishedDate { get; set; }
        public Guid? FinishedBy { get; set; }
        public Guid? ReOpenedBy { get; set; }
        public Guid? ClosedBy { get; set; }
        public DateTime? ClosedAtFrom { get; set; }
        public DateTime? ClosedAtTo { get; set; }
        public string? Status { get; set; }

    }
}
