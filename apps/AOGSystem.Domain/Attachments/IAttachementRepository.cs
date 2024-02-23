using AOGSystem.Domain;
using AOGSystem.Domain.Attachments;
using AOGSystem.Domain.SOA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.EntityConfigurations.Attachmetns
{
    public interface IAttachementRepository
    {
        Attachment Add(Attachment attachement);
        void Update(Attachment attachement);
        void DeleteAttachment(Guid id);
        Task<PaginatedList<Attachment>> GetAllAttachments(Expression<Func<Attachment, bool>> predicate, int page, int pageSize);
        Task<Attachment> GetAttachmentByIDAsync(Guid? id);
        Task<List<Attachment>> GetAttachmentByFileNameAsync(string fileName);

        AttachmentLink Add(AttachmentLink attachement);
        void Update(AttachmentLink attachement);
        void DeleteAttachmentLink(Guid id);
        Task<AttachmentLink> GetAttachmentLinkByIDAsync(Guid? id);
        Task<AttachmentLink> GetAttachmentLinkByAttachmentIdAsync(Guid attachmentId);
        Task<List<AttachmentLink>> GetAttachmentLinkByEntityIdAsync(Guid entityId, string entityType);
        Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default);

    }
}
