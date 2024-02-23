using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Assignments.Query.Model
{
    public class AssignmentQueryModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public Guid? StartBy { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ExpectedFinishedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public Guid? FinishedBy { get; set; }
        public Guid? AssignedTo { get; set; }
        public Guid? ReAssignedTo { get; set; }
        public Guid? ReAssignedBy { get; set; }
        public DateTime? ReAssignedAt { get; set; }
        public string? Status { get; set; }
        public Guid? ReOpenedBy { get;  set; }
        public DateTime? ReOpenedAt { get; set; }
        public Guid? ClosedBy { get; set; }
        public DateTime? ClosedAt { get; set; }
    }
}
