using AOGSystem.Application.CoreFollowUps.Commands.CostSaving;
using AOGSystem.Application.CoreFollowUps.Query.Model.CostSaving;
using AOGSystem.Application.SOA.Commands;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.CostSavings;
using AOGSystem.Domain.SOA;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

namespace AOGSystem.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CostSavingController : ControllerBase
    {
        private readonly ICostSavingRepository _costSavingRepository;
        private readonly IMediator _mediator;
        public CostSavingController(ICostSavingRepository costSavingRepository, IMediator mediator)
        {
            _costSavingRepository = costSavingRepository;
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCostSaving([FromBody] CreateCostSavingCommand command)
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

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateCostSaving([FromBody] UpdateCostSavingCommand command)
        {
            try
            {
                command.SetUpdateBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
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
        public async Task<IActionResult> GetAllCostSaving([FromQuery] CostSavingSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {

                Expression<Func<CostSaving, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.OldPO) || queryModel.OldPO.Contains(query.OldPO)) &&
                    (string.IsNullOrEmpty(query.NewPO) || queryModel.NewPO.Contains(query.NewPO)) &&
                    (!query.IssueDateFrom.HasValue || queryModel.IssueDate >= query.IssueDateFrom) &&
                    (!query.IssueDateTo.HasValue || queryModel.IssueDate <= query.IssueDateTo) &&
                    (!query.CNDateFrom.HasValue || queryModel.CNDate >= query.CNDateFrom) &&
                    (!query.CNDateTo.HasValue || queryModel.CNDate <= query.CNDateTo) &&
                    (!query.IsPurchaseOrder.HasValue || queryModel.IsPurchaseOrder == query.IsPurchaseOrder) &&
                    (!query.IsRepairOrder.HasValue || queryModel.IsRepairOrder == query.IsRepairOrder) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));


                var data = await _costSavingRepository.GetAllCostSavings(predicate, page, pageSize);
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

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCostSavingByID(Guid id)
        {
            try
            {
                return Ok(await _costSavingRepository.GetCostSavingByIDAsync(id));
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
        public async Task<IActionResult> GetActiveCostSavings()
        {
            try
            {
                return Ok(await _costSavingRepository.GetActiveCostSavings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{poNo}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCostSavingByNewPONo(string poNo)
        {
            try
            {
                return Ok(await _costSavingRepository.GetCostSavingByNewPONoAsync(poNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
