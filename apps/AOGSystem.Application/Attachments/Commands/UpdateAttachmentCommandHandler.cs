using AOGSystem.Application.Attachments.Query;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.Attachments;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Attachments.Commands
{
    public class UpdateAttachmentCommandHandler : IRequestHandler<UpdateAttachmentCommand, ReturnDto<AttachmentQueryModel>>
    {
        private readonly IAttachementRepository _attachementRepository;
        public UpdateAttachmentCommandHandler(IAttachementRepository attachementRepository)
        {
            _attachementRepository = attachementRepository;
        }

        public async Task<ReturnDto<AttachmentQueryModel>> Handle(UpdateAttachmentCommand request, CancellationToken cancellationToken)
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

                var attachment = await _attachementRepository.GetAttachmentByIDAsync(request.Id);
                attachment.SetFileName(request.FileName ?? name);
                attachment.SetFilePath(filePath);
                attachment.SetType(fileType);
                attachment.SetSize(request.File.Length);
                attachment.UpdatedBy = request.UpdateBy;
                attachment.UpdatedAT = DateTime.Now;
                _attachementRepository.Update(attachment);

                var attachmentLink = await _attachementRepository.GetAttachmentLinkByAttachmentIdAsync(attachment.Id); // new AttachmentLink(attachment.Id, attachment, request.EntityId, request.EntityType);
                attachmentLink.SetAttachmentId(attachment.Id);
                attachmentLink.SetAttachment(attachment);
                attachmentLink.SetEntityId(request.EntityId);
                attachmentLink.SetEntityType(request.EntityType);
                attachmentLink.UpdatedBy = request.UpdateBy;
                attachmentLink.UpdatedAT = DateTime.Now;

                _attachementRepository.Update(attachmentLink);

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
                    Message = "Attachment updated successfully",
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
    public class UpdateAttachmentCommand : IRequest<ReturnDto<AttachmentQueryModel>>
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }

        [JsonIgnore]
        public Guid? UpdateBy { get; private set; }
        public void SetUpdateBy(Guid updateBy) { UpdateBy = updateBy; }
    }
}
