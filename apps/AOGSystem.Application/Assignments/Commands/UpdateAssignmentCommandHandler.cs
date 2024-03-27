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
            model.SetTitle(request.Title != null ? request.Title : model.Title);
            model.SetDescription(request.Description != null ? request.Description : model.Description);
            model.SetStartDate(request.StartDate != null ? request.StartDate : model.StartDate);
            model.SetStartBy(request.StartBy != null ? request.StartBy : model.StartBy);
            model.SetDueDate(request.DueDate != null ? request.DueDate : model.DueDate);
            model.SetExpectedFinishedDate(request.ExpectedFinishedDate != null ? request.ExpectedFinishedDate : model.ExpectedFinishedDate);
            model.SetFinshedDate(request.FinishedDate != null ? request.FinishedDate : model.FinishedDate);
            model.SetFinshedBy(request.FinishedBy != null ? request.FinishedBy : model.FinishedBy);
            model.SetAssignedTo(request.AssignedTo != null ? request.AssignedTo : model.AssignedTo);
            model.SetReAssignedTo(request.ReAssignedTo != null ? request.ReAssignedTo : model.ReAssignedTo);
            model.SetReAssignedBy(request.ReAssignedBy != null ? request.ReAssignedBy : model.ReAssignedBy);
            model.SetReAssignedAt(request.ReAssignedAt != null ? request.ReAssignedAt : model.ReAssignedAt);
            model.SetStatus(request.Status != null ? request.Status : model.Status);

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
        public Guid Id { get; set; }
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
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }
    }
}
