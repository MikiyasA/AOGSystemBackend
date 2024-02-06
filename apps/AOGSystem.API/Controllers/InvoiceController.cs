using AOGSystem.Application.General.Commands.Part;
using AOGSystem.Application.Invoice.Commands;
using AOGSystem.Application.Invoice.Query;
using AOGSystem.Domain.Invoices;
using AOGSystem.Persistence.Repository.General;
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
    public class InvoiceController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceQuery _invoiceQuery;
        public InvoiceController(IMediator mediator, IHttpContextAccessor httpContextAccessor, IInvoiceRepository invoiceRepository, IInvoiceQuery invoiceQuery)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
            _invoiceRepository = invoiceRepository;
            _invoiceQuery = invoiceQuery;
        }


        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
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
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command)
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

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> InvoiceApproval([FromBody] InvoiceApprovalCommand command)
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
        public async Task<IActionResult> GetAllInvoices()
        {
            try
            {
                return Ok(await _invoiceRepository.GetAllInvoices());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceByID(int id)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoiceByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{orderId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceBySalesOrderId(int orderId)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoiceBySalesOrderIdAsync(orderId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{orderId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceByLoanOrderId(int orderId)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoiceByLoanOrderIdAsync(orderId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{invoiceNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceByInvoiceNo(string invoiceNo)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoiceByInvoiceNoAsync(invoiceNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpGet("{invoiceNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoicesByInvoiceNo(string invoiceNo)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoiceByInvoicesNoAsync(invoiceNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetApprovedlInvoices()
        {
            try
            {
                return Ok(await _invoiceRepository.GetApprovedlInvoices());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUnapprovedlInvoices()
        {
            try
            {
                return Ok(await _invoiceRepository.GetUnapprovedlInvoices());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetActiveInvoices()
        {
            try
            {
                return Ok(await _invoiceQuery.GetAllActiveInvoicesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{type}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoicesBytransactionType(string type)
        {
            try
            {
                return Ok(await _invoiceRepository.GetInvoicesBytransactionType(type));
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
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> InvoiceCloser([FromBody] InvoiceCloserCommand command)
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
    }
}
