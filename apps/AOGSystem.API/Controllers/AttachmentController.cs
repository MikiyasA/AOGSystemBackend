using AOGSystem.Application.Attachments.Commands;
using AOGSystem.Application.Attachments.Query;
using AOGSystem.Application.SOA.Commands;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.Attachments;
using AOGSystem.Domain.SOA;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace AOGSystem.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAttachementRepository _attachementRepository;
        public AttachmentController(IMediator mediator, IAttachementRepository attachementRepository)
        {
            _mediator = mediator;
            _attachementRepository = attachementRepository;
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UploadAttachment([FromForm] UploadttachmentCommand command)
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
        public async Task<IActionResult> UpdateAttachment([FromForm] UpdateAttachmentCommand command)
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

        //[HttpGet]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> GetAllAttachments([FromQuery] AttachmentSearchQuery query, int page = 1, int pageSize = 20)
        //{
        //    try
        //    {

        //        Expression<Func<Attachment, bool>> predicate = queryModel =>
        //            (string.IsNullOrEmpty(query.VendorName) || queryModel.VendorName.Contains(query.VendorName)) &&
        //            (string.IsNullOrEmpty(query.VendorCode) || queryModel.VendorCode.Contains(query.VendorCode)) &&
        //            (string.IsNullOrEmpty(query.VendorAccountManagerName) || queryModel.VendorAccountManagerName.Contains(query.VendorAccountManagerName)) &&
        //            (string.IsNullOrEmpty(query.VendorFinanceContactName) || queryModel.VendorFinanceContactName.Contains(query.VendorFinanceContactName)) &&
        //            (string.IsNullOrEmpty(query.ETFinanceContactName) || queryModel.ETFinanceContactName.Contains(query.ETFinanceContactName)) &&
        //            (!query.CertificateExpiryDateFrom.HasValue || queryModel.CertificateExpiryDate >= query.CertificateExpiryDateFrom) &&
        //            (!query.CertificateExpiryDateTo.HasValue || queryModel.CertificateExpiryDate <= query.CertificateExpiryDateTo) &&
        //            (string.IsNullOrEmpty(query.Status) || queryModel.Status.Contains(query.Status));


        //        var data = await _attachementRepository.GetAllAttachments(predicate, page, pageSize);
        //        var result = new
        //        {
        //            metadata = new
        //            {
        //                data.TotalCount,
        //                data.PageSize,
        //                data.CurrentPage,
        //                data.TotalPages,
        //            },
        //            data
        //        };
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex);
        //    }
        //}

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAttachmentById(Guid id)
        {
            try
            {
                return Ok(await _attachementRepository.GetAttachmentByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{fileName}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAttachmentByFileName(string fileName)
        {
            try
            {
                return Ok(await _attachementRepository.GetAttachmentByFileNameAsync(fileName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAttachmentLinkById(Guid id)
        {
            try
            {
                return Ok(await _attachementRepository.GetAttachmentLinkByIDAsync(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{attachmentId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAttachmentLinkByAttachmentId(Guid attachmentId)
        {
            try
            {
                return Ok(await _attachementRepository.GetAttachmentLinkByAttachmentIdAsync(attachmentId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{entityId}/{entityType}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAttachmentLinkByEntityId(Guid entityId, string entityType)
        {
            try
            {
                return Ok(await _attachementRepository.GetAttachmentLinkByEntityIdAsync(entityId, entityType));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{attachmentId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DownloadAttachment(Guid attachmentId)
        {
            try
            {
                var attachment = await _attachementRepository.GetAttachmentByIDAsync(attachmentId);
                if (attachment == null)
                    return NotFound("no attachment with this Id");

                var filePath = attachment.FilePath; // Get the file path from the attachment

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                    return NotFound("file not found");

                // Serve the file
                var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(fileBytes, "application/octet-stream", attachment.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
