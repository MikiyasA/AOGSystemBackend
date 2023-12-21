using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class Assignment : BaseEntity
    {
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? ExpectedFinishedDate { get; private set; }
        public DateTime? FinishedDate { get; private set; }
        public string? Status { get; private set; }

        public void SetTitle(string title) { this.Title = title; }
        public void SetDescription(string description) { this.Description = description; }
        public void SetStartDate(DateTime? startDate) { this.StartDate = startDate; }
        public void SetDueDate(DateTime? dueDate) { this.DueDate = dueDate; }
        public void SetExpectedFinishedDate(DateTime? expectedFinishedDate) { this.ExpectedFinishedDate = expectedFinishedDate; }
        public void SetFinshedDate(DateTime? finishedDate) { this.FinishedDate = finishedDate; }
        public void SetStatus(string status) { this.Status = status; }

        public Assignment() { }
        public Assignment(string title, string description, DateTime? startDate, DateTime dueDate, DateTime? expectedFinishedDate, DateTime? finshedDate, string status) : this()
        {
            this.SetTitle(title);
            this.SetDescription(description);
            this.SetStartDate(startDate);
            this.SetDueDate(dueDate);
            this.SetExpectedFinishedDate(expectedFinishedDate);
            this.SetFinshedDate(finshedDate); 
            this.SetStatus(status);
        }

        // AssignedTo, StartedBy, FinshedBy 
    }
}
