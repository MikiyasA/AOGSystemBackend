using AOGSystem.Application.General.Commands.Part;
using AOGSystem.Application.Invoice.Commands;
using AOGSystem.Application.Invoice.Query;
using AOGSystem.Application.Invoice.Query.Model;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.Invoices;
using AOGSystem.Persistence.Repository.General;
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


        [Authorize(Policy = "RequireAdminOrCoordinatorOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
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
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceCommand command)
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

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> InvoiceApproval([FromBody] InvoiceApprovalCommand command)
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

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllInvoices([FromQuery] InvoiceSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {

                Expression<Func<Invoice, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.InvoiceNo) || queryModel.InvoiceNo.Contains(query.InvoiceNo)) &&
                    (!query.InvoiceDateFrom.HasValue || queryModel.InvoiceDate >= query.InvoiceDateFrom) &&
                    (!query.InvoiceDateTo.HasValue || queryModel.InvoiceDate <= query.InvoiceDateTo) &&
                    (!query.DueDateFrom.HasValue || queryModel.DueDate >= query.DueDateFrom) &&
                    (!query.DueDateTo.HasValue || queryModel.DueDate <= query.DueDateTo) &&
                    (string.IsNullOrEmpty(query.TransactionType) || queryModel.TransactionType.Contains(query.TransactionType)) &&
                    (string.IsNullOrEmpty(query.POPReference) || queryModel.POPReference.Contains(query.POPReference)) &&
                    (!query.POPDateFrom.HasValue || queryModel.POPDate >= query.POPDateFrom) &&
                    (!query.POPDateTo.HasValue || queryModel.POPDate <= query.POPDateTo) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));


                var data = await _invoiceRepository.GetAllInvoices(predicate, page, pageSize);
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
        public async Task<IActionResult> GetInvoiceByID(Guid id)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceBySalesOrderId(Guid orderId)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceByLoanOrderId(Guid orderId)
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [Authorize(Policy = "RequireAdminOrTLRole")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> InvoiceCloser([FromBody] InvoiceCloserCommand command)
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
    }
}
