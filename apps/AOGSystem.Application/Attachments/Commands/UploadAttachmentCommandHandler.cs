using System;
using System.IO;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AOGSystem.Application.Attachments.Query;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.Attachments;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AOGSystem.Application.Attachments.Commands
{
    public class UploadAttachmentCommandHandler : IRequestHandler<UploadttachmentCommand, ReturnDto<AttachmentQueryModel>>
    {
        private readonly IAttachementRepository _attachementRepository;
        public UploadAttachmentCommandHandler(IAttachementRepository attachementRepository)
        {
            _attachementRepository = attachementRepository;
        }
        public async Task<ReturnDto<AttachmentQueryModel>> Handle(UploadttachmentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                    return new ReturnDto<AttachmentQueryModel>
                    {
                        Data = null,
                        Count = 0,
                        IsSuccess = false,
                        Message = "Invalid file used",
                    };

                var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                var fileName = Path.GetFileNameWithoutExtension(request.File.FileName) + " - " + Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                var filePath = Path.Combine(uploadsDir, fileName);
                var name = Path.GetFileNameWithoutExtension(request.File.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                var fileType = Path.GetExtension(fileName);

                var attachment = new Attachment(request.FileName ?? name, filePath, request.File.Length, fileType);
                attachment.CreatedAT = DateTime.Now;
                attachment.CreatedBy = request.CreatedAt;
                _attachementRepository.Add(attachment);
                await _attachementRepository.SaveChangesAsync();

                var attachmentLink = new AttachmentLink(attachment.Id, attachment, (Guid)request.EntityId, request.EntityType);
                attachmentLink.CreatedAT = DateTime.Now;
                attachmentLink.CreatedBy = request.CreatedAt;
                _attachementRepository.Add(attachmentLink);

                var result = await _attachementRepository.SaveChangesAsync();
                if (result == 0)
                    return new ReturnDto<AttachmentQueryModel>
                    {
                        Data = null,
                        Count = 0,
                        IsSuccess = false,
                        Message = "Something went wrong when attachment uploaded",
                    };

                var returnData = new AttachmentQueryModel
                {
                    FileName = attachment.FileName,
                    FilePath = attachment.FilePath,
                    Size = attachment.Size,
                    Type = attachment.Type,
                    AttachmentId = attachment.Id,
                    AttachmentLinkId = attachmentLink.Id,
                    EntityId = attachment.Id,
                    EntityType = attachment.Type,
                };

                return new ReturnDto<AttachmentQueryModel>
                {
                    Data = returnData,
                    Count = 1,
                    IsSuccess = true,
                    Message = "Attachment uploaded successfully",
                };
            }
            catch (Exception ex)
            {
                return new ReturnDto<AttachmentQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                };
            }
        }
    }

    public class UploadttachmentCommand : IRequest<ReturnDto<AttachmentQueryModel>>
    {
        public IFormFile? File { get; set; }
        public string? FileName { get; set; }
        public Guid? EntityId { get; set; }
        public string? EntityType { get; set; }

        [JsonIgnore]
        public Guid? CreatedAt { get; private set; }
        public void SetCreatedAt(Guid createdAt) { CreatedAt = createdAt; }
    }
}
