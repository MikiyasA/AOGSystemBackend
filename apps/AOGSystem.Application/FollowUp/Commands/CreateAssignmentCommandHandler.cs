using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.FollowUp;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
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
            var model = new Assignment(request.Title, request.Description, request.StartDate, request.DueDate, request.ExpectedFinishedDate,
                request.FinishedDate, request.Status);
            model.CreatedAT = DateTime.Now;
            _assignmentRepository.Add(model);
            var result = await _assignmentRepository.SaveChangesAsync();
            if (request == null)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "There is an error on created assignment"
                };
            var returnData = new AssignmentQueryModel
            {
                Title = request.Title,
                Description = request.Description,
                StartDate = request.StartDate,
                DueDate = request.DueDate,
                ExpectedFinishedDate = request.ExpectedFinishedDate,
                FinishedDate = request.FinishedDate,
                Status = request.Status
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
        public DateTime? StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ExpectedFinishedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string Status { get; set; }
    }
}
