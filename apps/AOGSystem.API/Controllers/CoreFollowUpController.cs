using AOGSystem.Application.CoreFollowUps.Commands;
using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCoreFollowUp([FromBody] CreateCoreFollowUpCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCoreFollowUp([FromBody] UpdateCoreFollowUpCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllCoreFollowUp([FromQuery] FilterCoreQuery query)
        {
            try
            {
                var data = await _coreFollowUpRepository.GetAllCoreFollowUps(query.Page, query.PageSize);

                //if (query.CreatedStartDate != null)
                //    data = data.Where(d => d.POCreatedDate >= query.CreatedStartDate).ToList();
                //if (query.CreatedEndDate != null)
                //    data = data.Where(d => d.POCreatedDate <= query.CreatedEndDate).ToList();
                //if (query.ReturnStartDate != null)
                //    data = data.Where(d => d.ReturnProcessedDate >= query.ReturnStartDate).ToList();
                //if (query.ReturnEndDate != null)
                //    data = data.Where(d => d.ReturnProcessedDate <= query.ReturnEndDate).ToList();
                //if (query.PODStartDate != null)
                //    data = data.Where(d => d.PODDate >= query.PODStartDate).ToList();
                //if (query.PODEndDate != null)
                //    data = data.Where(d => d.PODDate <= query.PODEndDate).ToList();
                if(query.Status != null)
                    data = data.Where(d => d.Status == query.Status).ToList();

                if (query.AirCraft != null)
                    data = data.Where(d => d.Aircraft == query.AirCraft).ToList();
                if (query.TailNo != null)
                    data = data.Where(d => d.TailNo == query.TailNo).ToList();
                if (query.PN != null)
                    data = data.Where(d => d.PartNumber == query.PN).ToList();
                if (query.Vendor != null)
                    data = data.Where(d => d.Vendor == query.Vendor).ToList();
                if (query.AWBNo != null)
                    data = data.Where(d => d.AWBNo == query.AWBNo).ToList();
                if (query.ReturnPart != null)
                    data = data.Where(d => d.ReturnedPart == query.ReturnPart).ToList();


                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCoreFollowUpByID(int id)
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
