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
    public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, ReturnDto<AssignmentQueryModel>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMediator _mediator;
        public CreateAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMediator mediator)
        {
            _assignmentRepository = assignmentRepository;
            _mediator = mediator;
        }

        public async Task<ReturnDto<AssignmentQueryModel>> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
        {
            var model = new Assignment(request.Title, request.Description, request.DueDate, request.ExpectedFinishedDate,
                request.AssignedTo);
            model.SetStatus("Created");
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _assignmentRepository.Add(model);
            var result = await _assignmentRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "There is an error on created assignment"
                };
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
                Message = "Assignment created successfully"
            };


        }
    }

    public class CreateAssignmentCommand : IRequest<ReturnDto<AssignmentQueryModel>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ExpectedFinishedDate { get; set; }
        public Guid? AssignedTo { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
