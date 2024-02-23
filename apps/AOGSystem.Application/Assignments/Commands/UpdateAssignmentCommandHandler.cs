using AOGSystem.Application.Assignments.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Assignments.Commands
{
    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, ReturnDto<AssignmentQueryModel>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMediator _mediator;
        public UpdateAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMediator mediator)
        {
            _assignmentRepository = assignmentRepository;
            _mediator = mediator;
        }

        public async Task<ReturnDto<AssignmentQueryModel>> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var model = await _assignmentRepository.GetAssignmentById(request.Id);
            if (model == null)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "Assignment can not be found"
                };
            model.SetTitle(request.Title);
            model.SetDescription(request.Description);
            model.SetStartDate(request.StartDate);
            model.SetStartBy(request.StartBy);
            model.SetDueDate(request.DueDate);
            model.SetExpectedFinishedDate(request.ExpectedFinishedDate);
            model.SetFinshedDate(request.FinishedDate);
            model.SetFinshedBy(request.FinishedBy);
            model.SetAssignedTo(request.AssignedTo);
            model.SetReAssignedTo(request.ReAssignedTo);
            model.SetReAssignedBy(request.ReAssignedBy);
            model.SetReAssignedAt(request.ReAssignedAt);
            model.SetStatus(request.Status);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _assignmentRepository.Update(model);

            var result = await _assignmentRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "There is an error on update assignment"
                }; ;
            var returnData = new AssignmentQueryModel
            {
                Title = model.Title,
                Description = model.Description,
                StartDate = model.StartDate,
                StartBy = model.StartBy,
                DueDate = model.DueDate,
                ExpectedFinishedDate = model.ExpectedFinishedDate,
                FinishedDate = model.FinishedDate,
                FinishedBy = model.FinishedBy,
                AssignedTo = model.AssignedTo,
                ReAssignedTo = model.ReAssignedTo,
                ReAssignedBy = model.ReAssignedBy,
                ReAssignedAt = model.ReAssignedAt,
                Status = model.Status,
                ReOpenedBy = model.ReOpenedBy,
                ReOpenedAt = model.ReOpenedAt,
            };

            return new ReturnDto<AssignmentQueryModel>
            {
                Data = returnData,
                IsSuccess = true,
                Count = 1,
                Message = "Assignment updated successfully"
            };

        }
    }

    public class UpdateAssignmentCommand : IRequest<ReturnDto<AssignmentQueryModel>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
        public string Status { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
