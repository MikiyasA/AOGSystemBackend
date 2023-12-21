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
            if(model == null)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "This Assignment can not be found"
                };
            model.SetTitle(request.Title);
            model.SetDescription(request.Description);
            model.SetStartDate(request.StartDate);
            model.SetDueDate(request.DueDate);
            model.SetExpectedFinishedDate(request.ExpectedFinishedDate);
            model.SetFinshedDate(request.FinishedDate);
            model.SetStatus(model.Status);
            model.UpdatedAT = DateTime.Now;

            _assignmentRepository.Update(model);

            var result = _assignmentRepository.SaveChangesAsync();
            if (request == null)
                return new ReturnDto<AssignmentQueryModel>
                {
                    Data = null,
                    IsSuccess = false,
                    Count = 0,
                    Message = "There is an error on update assignment"
                }; ;
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
                IsSuccess= true,
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
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpectedFinishedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public string Status { get; set; }
    }
}
