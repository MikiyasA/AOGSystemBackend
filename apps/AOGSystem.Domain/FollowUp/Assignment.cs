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
        public Guid? StartBy { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime? ExpectedFinishedDate { get; private set; }
        public DateTime? FinishedDate { get; private set; }
        public Guid? FinishedBy { get; private set; }
        public Guid? AssignedTo { get; private set; }
        public Guid? ReAssignedTo { get; private set; }
        public Guid? ReAssignedBy { get; private set; }
        public DateTime? ReAssignedAt { get; private set; }
        public string? Status { get; private set; }
        public Guid? ReOpenedBy { get; private set; }
        public DateTime? ReOpenedAt { get; private set; }
        public Guid? ClosedBy { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        public void SetTitle(string title) { Title = title; }
        public void SetDescription(string description) { Description = description; }
        public void SetStartDate(DateTime? startDate) { StartDate = startDate; }
        public void SetStartBy(Guid? startBy) { StartBy = startBy; }
        public void SetDueDate(DateTime? dueDate) { DueDate = dueDate; }
        public void SetExpectedFinishedDate(DateTime? expectedFinishedDate) { ExpectedFinishedDate = expectedFinishedDate; }
        public void SetFinshedDate(DateTime? finishedDate) { FinishedDate = finishedDate; }
        public void SetFinshedBy(Guid? finishedBy) { FinishedBy = finishedBy; }
        public void SetAssignedTo(Guid? assignedTo) { AssignedTo = assignedTo;  }
        public void SetReAssignedTo(Guid? reAssignedTo) { ReAssignedTo =  reAssignedTo; }
        public void SetReAssignedBy(Guid? reAssignedBy) { ReAssignedBy = reAssignedBy; }
        public void SetReAssignedAt(DateTime? reAssignedAt) {  ReAssignedAt = reAssignedAt; }
        public void SetStatus(string status) { Status = status; }
        public void SetReopeenedBy(Guid? reopnedBy) { ReOpenedBy = reopnedBy; }
        public void SetReOpenedAt(DateTime? reopnedAt) { ReOpenedAt = reopnedAt; }
        public void SetClosedBy(Guid? closedBy) { ClosedBy = closedBy; }
        public void SetClosedAt(DateTime? closedAt) { ClosedAt = closedAt; }

        public Assignment() { }
        public Assignment(string title, string description, DateTime dueDate, DateTime? expectedFinishedDate, Guid? assignedTo) : this()
        {
            SetTitle(title);
            SetDescription(description);
            SetDueDate(dueDate);
            SetExpectedFinishedDate(expectedFinishedDate);
            SetAssignedTo(assignedTo);
        }

    }
}
