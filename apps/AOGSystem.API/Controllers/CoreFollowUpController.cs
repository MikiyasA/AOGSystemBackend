using AOGSystem.Application.CoreFollowUps.Commands;
using AOGSystem.Application.CoreFollowUps.Query.Model;
using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.FollowUp;
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
    public class CoreFollowUpController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        public CoreFollowUpController(IMediator mediator,
            ICoreFollowUpRepository coreFollowUpRepository)
        {
            _mediator = mediator;
            _coreFollowUpRepository= coreFollowUpRepository;
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCoreFollowUp([FromBody] CreateCoreFollowUpCommand command)
        {
            try
            {
                command.SetCreatedBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : BadRequest();
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
        public async Task<IActionResult> UpdateCoreFollowUp([FromBody] UpdateCoreFollowUpCommand command)
        {
            try
            {
                command.SetUpdatedBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCoreFollowUp([FromBody] DeleteCoreFollowUpCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult > 0 ? Ok($"{commandResult} - requested object deleted successfully") : BadRequest();
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
        public async Task<IActionResult> GetAllCoreFollowUp([FromQuery] CoreFollowupSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {
                Expression<Func<CoreFollowUp, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.PONo) || queryModel.PONo.Contains(query.PONo)) &&
                    (!query.POCreatedDateFrom.HasValue || queryModel.POCreatedDate >= query.POCreatedDateFrom) &&
                    (!query.POCreatedDateTo.HasValue || queryModel.POCreatedDate <= query.POCreatedDateTo) &&
                    (string.IsNullOrEmpty(query.Aircraft) || queryModel.Aircraft.Contains(query.Aircraft)) &&
                    (string.IsNullOrEmpty(query.TailNo) || queryModel.TailNo.Contains(query.TailNo)) &&
                    (string.IsNullOrEmpty(query.PartNumber) || queryModel.PartNumber.Contains(query.PartNumber)) &&
                    (string.IsNullOrEmpty(query.StockNo) || queryModel.StockNo.Contains(query.StockNo)) &&
                    (string.IsNullOrEmpty(query.Vendor) || queryModel.Vendor.Contains(query.Vendor)) &&
                    (string.IsNullOrEmpty(query.AWBNo) || queryModel.AWBNo.Contains(query.AWBNo)) &&
                    (string.IsNullOrEmpty(query.ReturnedPart) || queryModel.ReturnedPart.Contains(query.ReturnedPart)) &&
                    (!query.PODDateFrom.HasValue || queryModel.PODDate >= query.PODDateFrom) &&
                    (!query.PODDateTo.HasValue || queryModel.PODDate <= query.PODDateTo) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));
                var data = await  _coreFollowUpRepository.GetAllCoreFollowUps(predicate, page, pageSize);
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
        public async Task<IActionResult> GetActiveCoreFollowUps()
        {
            try
            {
                return Ok(await _coreFollowUpRepository.GetActiveCoreFollowUps());
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
        public async Task<IActionResult> GetCoreFollowUpByID(Guid id)
        {
            try
            {
                var result = await _coreFollowUpRepository.GetCoreFollowUpByIDAsync(id);
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

        [HttpGet("{pONo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoreFollowUpPONo(string pONo)
        {
            try
            {
                var result = await _coreFollowUpRepository.GetCoreFollowUpByPONoAsync(pONo);
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

    }
}
