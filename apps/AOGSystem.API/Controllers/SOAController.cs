using AOGSystem.Application.Invoice.Commands;
using AOGSystem.Application.Invoice.Query.Model;
using AOGSystem.Application.SOA.Commands;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.SOA;
using AOGSystem.Persistence.Repository.Invoices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using System.Text.RegularExpressions;

namespace AOGSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class SOAController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IInvoiceListRepository _invoiceListRepository;
        private readonly IVendorRepository _vendorRepository;
        public SOAController(IMediator mediator, IInvoiceListRepository invoiceRepository, IVendorRepository vendorRepository)
        {
            _mediator = mediator;
            _invoiceListRepository = invoiceRepository;
            _vendorRepository = vendorRepository;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSOAVendor([FromBody] CreateVendorCommand command)
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
        public async Task<IActionResult> UpdateSOAVendor([FromBody] UpdateVendorCommand command)
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
        public async Task<IActionResult> GetAllSOAVendor([FromQuery] VendorSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {

                Expression<Func<Vendor, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.VendorName) || queryModel.VendorName.Contains(query.VendorName)) &&
                    (string.IsNullOrEmpty(query.VendorCode) || queryModel.VendorCode.Contains(query.VendorCode)) &&
                    (string.IsNullOrEmpty(query.VendorAccountManagerName) || queryModel.VendorAccountManagerName.Contains(query.VendorAccountManagerName)) &&
                    (string.IsNullOrEmpty(query.VendorFinanceContactName) || queryModel.VendorFinanceContactName.Contains(query.VendorFinanceContactName)) &&
                    (string.IsNullOrEmpty(query.ETFinanceContactName) || queryModel.ETFinanceContactName.Contains(query.ETFinanceContactName)) &&
                    (!query.CertificateExpiryDateFrom.HasValue || queryModel.CertificateExpiryDate >= query.CertificateExpiryDateFrom) &&
                    (!query.CertificateExpiryDateTo.HasValue || queryModel.CertificateExpiryDate <= query.CertificateExpiryDateTo) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));


                var data = await _vendorRepository.GetAllVendorSOA(predicate, page, pageSize);
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSOAVendorByID(Guid id)
        {
            try
            {
                return Ok(await _vendorRepository.GetVendorSOAByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{code}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSOAVendorSOAByCodeId(string code)
        {
            try
            {
                return Ok(await _vendorRepository.GetVendorSOAByCodeAsync(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetSOAVendorSOAByNameId(string name)
        {
            try
            {
                return Ok(await _vendorRepository.GetVendorSOAByNameAsync(name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAllActiveSOAVendors()
        {
            try
            {
                return Ok(await _vendorRepository.GetAllActiveVendorSOAAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        /// //////////////////////////////////////// Invoice List //////////////////////////////////////////////////////////

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddInvoiceList([FromBody] AddInvoiceListCommand command)
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
        public async Task<IActionResult> UpdateInvoiceList([FromBody] UpdateInvoiceListCommand command)
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
        public async Task<IActionResult> GetAllInvoiceLists([FromQuery] InvoiceListSearchQuery query, int page = 1, int pageSize = 20)
        {
            try
            {

                Expression<Func<InvoiceList, bool>> predicate = queryModel =>
                    (string.IsNullOrEmpty(query.InvoiceNo) || queryModel.InvoiceNo.Contains(query.InvoiceNo)) &&
                    (!query.InvoiceDateFrom.HasValue || queryModel.InvoiceDate >= query.InvoiceDateFrom) &&
                    (!query.InvoiceDateTo.HasValue || queryModel.InvoiceDate <= query.InvoiceDateTo) &&
                    (!query.DueDate.HasValue || queryModel.DueDate == query.DueDate) &&
                    (!query.POPDateFrom.HasValue || queryModel.POPDate >= query.POPDateFrom) &&
                    (!query.POPDateTo.HasValue || queryModel.POPDate <= query.POPDateTo) &&
                    (string.IsNullOrEmpty(query.POPReference) || queryModel.POPReference.Contains(query.POPReference)) &&
                    (string.IsNullOrEmpty(query.ChargeType) || queryModel.ChargeType.Contains(query.ChargeType)) &&
                    (string.IsNullOrEmpty(query.BuyerName) || queryModel.BuyerName.Contains(query.BuyerName)) &&
                    (string.IsNullOrEmpty(query.TLName) || queryModel.TLName.Contains(query.TLName)) &&
                    (string.IsNullOrEmpty(query.ManagerName) || queryModel.ManagerName.Contains(query.ManagerName)) &&
                    (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));


                var data = await _invoiceListRepository.GetAllSOAInvoiceList(predicate, page, pageSize);
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceListByID(Guid id)
        {
            try
            {
                return Ok(await _invoiceListRepository.GetSOAInvoiceListByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{orderNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceListByOrderNo(string orderNo)
        {
            try
            {
                return Ok(await _invoiceListRepository.GetSOAInvoiceListByOrderNoAsync(orderNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{invoiceNo}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetInvoiceListByInvoiceNo(string invoiceNo)
        {
            try
            {
                return Ok(await _invoiceListRepository.GetSOAInvoiceListByInvoiceNoAsync(invoiceNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> etAllActiveSOAInvoiceList()
        {
            try
            {
                return Ok(await _invoiceListRepository.GetAllActiveSOAInvoiceListAsync());
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
        public async Task<IActionResult> ImportInvoiceList([FromForm] ImportInvoiceListCommand command)
        {
            try
            {
                var appRoot = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TempFiles");

                if (!Directory.Exists(appRoot))
                {
                    Directory.CreateDirectory(appRoot);
                }

                string[] allowedExtensions = { ".xls", ".xlsx", ".csv" };
                var uploadedFile = command.File;
                var extension = Path.GetExtension(uploadedFile.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    return BadRequest("Invalid file format.");
                }

                string fileName = $"{Guid.NewGuid()}{extension}";
                string filePath = Path.Combine(appRoot, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(stream);
                }

                command.FileDirectory = filePath;
                command.Extension = extension;

                var commandResult = await _mediator.Send(command);

                // Remove Temp Excel File
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                return commandResult != null ? Ok(commandResult) : BadRequest("Failed to import invoice list.");
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
        public async Task<IActionResult> AddBuyerRemark([FromBody] AddBuyerRemarkCommand command)
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
        public async Task<IActionResult> UpdateBuyerRemark([FromBody] UpdateBuyerRemarkCommand command)
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
        public async Task<IActionResult> AddFinanceRemark([FromBody] AddFinanceRemarkCommand command)
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
        public async Task<IActionResult> UpdateFinanceRemark([FromBody] UpdateFinanceRemarkCommand command)
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
