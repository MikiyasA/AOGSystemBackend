using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class AssignmentQueryModel
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ExpectedFinishedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string? Status { get; set; }
    }
}
