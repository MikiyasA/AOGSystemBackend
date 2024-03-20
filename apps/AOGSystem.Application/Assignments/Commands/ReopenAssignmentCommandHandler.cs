using AOGSystem.Application.Assignments.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Assignments.Commands
{
    public class ReopenAssignmentCommandHandler : IRequestHandler<ReopenAssignmentCommand, ReturnDto<AssignmentQueryModel>>
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMediator _mediator;
        public ReopenAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMediator mediator)
        {
            _assignmentRepository = assignmentRepository;
            _mediator = mediator;
        }

        public async Task<ReturnDto<AssignmentQueryModel>> Handle(ReopenAssignmentCommand request, CancellationToken cancellationToken)
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

            model.SetReopeenedBy(request.ReOpenedBy);
            model.SetReOpenedAt(DateTime.Now);
            model.SetStatus("Re-opened");

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
                Message = "Assignment reopened successfully"
            };
        }
    }

    public class ReopenAssignmentCommand : IRequest<ReturnDto<AssignmentQueryModel>>
    {
        public Guid Id { get; set; }
        public Guid? ReOpenedBy { get; set; }
    }
}
