using AOGSystem.Application.FollowUp.Commands;
using AOGSystem.Domain.FollowUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AOGSystem.API.Controllers.AOGFollowUp
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class AOGFollowUpController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        public AOGFollowUpController(IMediator mediator, 
            IAOGFollowUpRepository AOGFollowUpRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _AOGFollowUpRepository = AOGFollowUpRepository;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAOGFollowUp([FromBody]CreateAOGFPCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateAOGFollowUp([FromBody] UpdateAOGFPCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteAOGFollowUp([FromBody] DeleteAOGFPCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult > 0 ? Ok(commandResult) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllAOGFollowUp()
        {
            try
            {
                return Ok(await _AOGFollowUpRepository.GetAllAOGFollowUpAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllActiveFollowUpAsync()
        {
            try
            {
                return Ok(await _AOGFollowUpRepository.GetAllActiveFollowUpAsync());
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
        public async Task<IActionResult> GetAOGFollowUpByID(int id)
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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddRemarkInAOGFollowUp([FromBody] AddRemarkInAOGFPCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateRemarkInAOGFollowUp([FromBody] UpdateRemarkInAOGFPCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                return commandResult != null ? Ok(commandResult) : (IActionResult)BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
