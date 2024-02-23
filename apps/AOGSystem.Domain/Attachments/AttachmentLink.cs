using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Attachments
{
    public class AttachmentLink : BaseEntity
    {
        public Guid AttachmentId { get; private set; }
        public Attachment Attachment { get; private set; }

        public Guid EntityId { get; private set; }
        public string EntityType { get; private set; }

        public void SetAttachmentId(Guid attachmentId) { AttachmentId = attachmentId; }
        public void SetAttachment(Attachment attachment) { Attachment = attachment; }
        public void SetEntityId(Guid entityId) {  EntityId = entityId; }
        public void SetEntityType(string entityType) {  EntityType = entityType; }

        public AttachmentLink() { }
        public AttachmentLink(Guid attachmentId, Attachment attachment, Guid entityId, string entityType) : this()
        {  
            AttachmentId = attachmentId;
            Attachment = attachment;
            EntityId = entityId;
            EntityType = entityType;
        }
    }
}
