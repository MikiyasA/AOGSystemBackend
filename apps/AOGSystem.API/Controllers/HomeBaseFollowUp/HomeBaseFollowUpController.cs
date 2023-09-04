using AOGSystem.Application.FollowUp.HomeBase.Commands;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AOGSystem.API.Controllers.HomeBaseFollowUp
{
    public class HomeBaseFollowUpController : Controller
    {
        private readonly IMediator _mediator;
        public HomeBaseFollowUpController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateHomeBaseFollowUp([FromBody]CreateHomeBaseFPCommand command)
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
