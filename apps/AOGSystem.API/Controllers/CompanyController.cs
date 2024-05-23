using AOGSystem.Application.General.Commands.Company;
using AOGSystem.Application.General.Commands.Part;
using AOGSystem.Domain.General;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace AOGSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class CompanyController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(IMediator mediator, ICompanyRepository companyRepository)
        {
            _mediator = mediator;
            _companyRepository = companyRepository;
        }

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
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

        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand command)
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
        public async Task<IActionResult> DeleteCompany([FromBody] DeleteCompanyCommand command)
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
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                return Ok(await _companyRepository.GetAllCompanyAsync());
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
        public async Task<IActionResult> GetCompanyByID(Guid id)
        {
            try
            {
                var result = await _companyRepository.GetCompanyByIDAsync(id);
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

        [HttpGet("{code}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCompanyByCode(string code)
        {
            try
            {
                var result =  _companyRepository.GetCompanyByCode(code);
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

        [HttpGet("{name}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetCompanyByName(string name)
        {
            try
            {
                var result = _companyRepository.GetCompanyByName(name);
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
