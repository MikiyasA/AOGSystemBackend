using AOGSystem.Application.General.Commands.Part;
using AOGSystem.Application.Sales.Command;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Sales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AOGSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class SalesController : Controller
    {        
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISaleRepository _saleRepository;
        private readonly ISalePartListRepository _salePartListRepository;
        public SalesController(IMediator mediator, IHttpContextAccessor httpContextAccessor, ISaleRepository saleRepository, ISalePartListRepository salePartListRepository)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _saleRepository = saleRepository;
            _salePartListRepository = salePartListRepository;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSales([FromBody] CreateSalesCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSales([FromBody] UpdateSalesCommand command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                return Ok(await _saleRepository.GetAllSales());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSalesByID(int id)
        {
            try
            {
                return Ok(await _saleRepository.GetSalesByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSalesByCompanyId(int companyId)
        {
            try
            {
                return Ok(await _saleRepository.GetSalesByCompanyIdAsync(companyId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{orderNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSalesByOrderNoAsync(string orderNo)
        {
            try
            {
                return Ok(await _saleRepository.GetSalesByOrderNoAsync(orderNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{customerOrderNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSalesByCustomerOrderNoAsync(string customerOrderNo)
        {
            try
            {
                return Ok(await _saleRepository.GetSalesByCustomerOrderNoAsync(customerOrderNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllActiveSales()
        {
            try
            {
                return Ok(await _saleRepository.GetAllActiveAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        ///////////////////////////// ///

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSalesPartList([FromBody] AddSalesPartListInSalesCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSalesPartList([FromBody] UpdateSalesPartListInSalesCommand command)
        {
            try
            {
                //command.SetUpdatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllSalesPartList()
        {
            try
            {
                return Ok(await _salePartListRepository.GetAllSalesPartLists());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSalesPartListByID(int id)
        {
            try
            {
                return Ok(await _salePartListRepository.GetSalesPartListByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ShipSales([FromBody] ShipSalesCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SalesApproval([FromBody] SalesApprovalCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var commandResult = await _mediator.Send(command);

                return commandResult != null ? commandResult.IsSuccess ? Ok(commandResult) : BadRequest(commandResult) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SalesCloser([FromBody] SalesCloserCommand command)
        {
            try
            {
                //command.SetCreatedBy(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

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
