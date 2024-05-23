using AOGSystem.Application.Assignments.Commands;
using AOGSystem.Application.Assignments.Query.Model;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Persistence.Repository.FollowUp;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

namespace AOGSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class AssignmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentController(IMediator mediator, IAssignmentRepository assignmentRepository)
        {
            _mediator = mediator;
            _assignmentRepository = assignmentRepository;
        }

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentCommand command)
        {
            try
            {
                command.SetCreatedBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAssignments([FromQuery] AssignmentSearchQuery query, int page=1, int pageSize = 20)
        {
            try
            {
                Expression<Func<Assignment, bool>> predicate = assignment =>
                        (string.IsNullOrEmpty(query.Title) || assignment.Title.Contains(query.Title)) &&
                        (!query.CreatedAtFrom.HasValue || assignment.CreatedAT >= query.CreatedAtFrom) &&
                        (!query.CreatedAtTo.HasValue || assignment.CreatedAT <= query.CreatedAtTo) &&
                        (!query.AssignedTo.HasValue || assignment.AssignedTo == query.AssignedTo) &&
                        (!query.StartBy.HasValue || assignment.StartBy == query.StartBy) &&
                        (!query.ReAssignedTo.HasValue || assignment.ReAssignedTo == query.ReAssignedTo) &&
                        (!query.ReAssignedBy.HasValue || assignment.ReAssignedBy == query.ReAssignedBy) &&
                        (!query.FinishedDate.HasValue || assignment.FinishedDate == query.FinishedDate) && 
                        (!query.FinishedBy.HasValue || assignment.FinishedBy == query.FinishedBy) && 
                        (!query.ReOpenedBy.HasValue || assignment.ReOpenedBy == query.ReOpenedBy) && 
                        (!query.ClosedBy.HasValue || assignment.ClosedBy == query.ClosedBy) && 
                        (!query.ClosedAtFrom.HasValue || assignment.ClosedAt >= query.ClosedAtFrom) &&
                        (!query.ClosedAtTo.HasValue || assignment.ClosedAt <= query.ClosedAtTo) &&
                        (string.IsNullOrEmpty(query.Status) || assignment.Status.Contains(query.Status));

                var data = await _assignmentRepository.GetAllAssignment(predicate, page, pageSize);

                var result = new
                {
                    metadata = new
                    {
                        data.TotalCount,
                        data.PageSize,
                        data.CurrentPage,
                        data.TotalPages,
                    },
                    data
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveAssignments()
        {
            try
            {
                return Ok(await _assignmentRepository.GetActiveAssignment());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveAssignmentByUserId() 
        {
            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(await _assignmentRepository.GetActiveAssignmentByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAssignmentUpByID(Guid id)
        {
            try
            {
                var result = await _assignmentRepository.GetAssignmentById(id);
                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAssignment([FromBody] UpdateAssignmentCommand command)
        {
            try
            {
                command.SetUpdatedBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> StartAssignment([FromBody] StartAssignmentCommand command)
        {
            try
            {
                command.StartBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FinishedAssignment([FromBody] FinishedAssignmentCommand command)
        {
            try
            {
                command.FinishedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReassignAssignment([FromBody] ReassignAssignmentCommand command)
        {
            try
            {
                command.ReAssignedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReopenAssignment([FromBody] ReopenAssignmentCommand command)
        {
            try
            {
                command.ReOpenedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CloseAssignment([FromBody] CloseAssignmentCommand command)
        {
            try
            {
                command.ClosedBy = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
