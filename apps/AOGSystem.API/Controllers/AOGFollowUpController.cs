using AOGSystem.Application.FollowUp.Commands;
using AOGSystem.Application.FollowUp.Query;
using AOGSystem.Application.FollowUp.Query.Model;
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
    public class AOGFollowUpController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        private readonly IActiveAOGFollowupQuery _activeAOGFollowupQuery;
        private readonly IFollowUpTabsRepository _followUpTabsRepository;
        public AOGFollowUpController(IMediator mediator,
            IAOGFollowUpRepository AOGFollowUpRepository,
            IActiveAOGFollowupQuery activeAOGFollowupQuery,
            IFollowUpTabsRepository followUpTabsRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _AOGFollowUpRepository = AOGFollowUpRepository;
            _activeAOGFollowupQuery = activeAOGFollowupQuery;
            _followUpTabsRepository = followUpTabsRepository;
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateFollowUpTab([FromBody] CreateFPTabCommand command)
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllFollowUpTabs()
        {
            try
            {
                return Ok(await _followUpTabsRepository.GetAllFollowUpTabsAsync());
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
        public async Task<IActionResult> GetAllActiveFollowUpTabs()
        {
            try
            {
                return Ok(await _activeAOGFollowupQuery.GetAllActiveFollowUpTabsAsync());
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
        public async Task<IActionResult> GetFollowTabUpByID(Guid id)
        {
            try
            {
                var result = await _followUpTabsRepository.GetFollowUpTabsByIDAsync(id);
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
        public async Task<IActionResult> UpdateFollowUpTab([FromBody] UpdateFPTabCommand command)
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
        public async Task<IActionResult> CreateAOGFollowUp([FromBody] CreateAOGFPCommand command)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAOGFollowUp([FromBody] UpdateAOGFPCommand command)
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
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAOGFollowUp([FromBody] DeleteAOGFPCommand command)
        {
            try
            {
                command.SetUpdatedBy(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
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
        public async Task<IActionResult> GetAllAOGFollowUps([FromQuery] FollowupSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {
                Expression<Func<AOGFollowUp, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.RID) || queryModel.RID.Contains(query.RID)) &&
                    (!query.RequestDateFrom.HasValue || queryModel.RequestDate >= query.RequestDateFrom) &&
                    (!query.RequestDateTo.HasValue || queryModel.RequestDate <= query.RequestDateTo) &&
                    (string.IsNullOrEmpty(query.AirCraft) || queryModel.AirCraft.Contains(query.AirCraft)) &&
                    (string.IsNullOrEmpty(query.TailNo) || queryModel.TailNo.Contains(query.TailNo)) &&
                    (string.IsNullOrEmpty(query.WorkLocation) || queryModel.WorkLocation.Contains(query.WorkLocation)) &&
                    (string.IsNullOrEmpty(query.AOGStation) || queryModel.AOGStation.Contains(query.AOGStation)) &&
                    (string.IsNullOrEmpty(query.Customer) || queryModel.Customer.Contains(query.Customer)) &&
                    (string.IsNullOrEmpty(query.PONumber) || queryModel.PONumber.Contains(query.PONumber)) &&
                    (string.IsNullOrEmpty(query.OrderType) || queryModel.OrderType.Contains(query.OrderType)) &&
                    (string.IsNullOrEmpty(query.Vendor) || queryModel.Vendor.Contains(query.Vendor)) &&
                    (string.IsNullOrEmpty(query.AWBNo) || queryModel.AWBNo.Contains(query.AWBNo)) &&
                    (string.IsNullOrEmpty(query.FlightNo) || queryModel.FlightNo.Contains(query.FlightNo)) &&
                    (!query.NeedHigherMgntAttn.HasValue || queryModel.NeedHigherMgntAttn == query.NeedHigherMgntAttn) &&
                    (!query.PartId.HasValue || queryModel.PartId == query.PartId) &&
                    (!query.FollowUpTabsId.HasValue || queryModel.FollowUpTabsId == query.FollowUpTabsId) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));

                var data = await _AOGFollowUpRepository.GetAllAOGFollowUpAsync(predicate, page, pageSize);
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
        public async Task<IActionResult> GetAllActiveFollowUps()
        {
            try
            {
                return Ok(await _activeAOGFollowupQuery.GetAllActiveFollowUpAsync());
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
        public async Task<IActionResult> GetAOGFollowUpByID(Guid id)
        {
            try
            {
                var result = await _AOGFollowUpRepository.GetAOGFollowUpByIDAsync(id);
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
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddRemarkInAOGFollowUp([FromBody] AddRemarkInAOGFPCommand command)
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
        public async Task<IActionResult> UpdateRemarkInAOGFollowUp([FromBody] UpdateRemarkInAOGFPCommand command)
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
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ExportAOGFPTOExcel()
        {
            try
            {
                byte[] excelData = await _mediator.Send(new ExportAOGFPTOExcelCommand());

                if (excelData != null)
                {
                    var today = DateTime.Now;

                    string date = today.ToString("dd-MMM-yyyy");
                    int hour = int.Parse(today.ToString("HH"));

                    string shift = hour < 3 ? "Evening" : hour < 9 ? "Night" : hour < 18 ? "Day" : "";

                    var file = File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"AOG Part Status For {date} {shift} Shift.xlsx");
                    return file;
                }
                else
                {
                    return BadRequest("Error exporting Excel data");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


    }
}
