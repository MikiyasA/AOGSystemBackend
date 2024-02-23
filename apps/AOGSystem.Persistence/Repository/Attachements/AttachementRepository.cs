using AOGSystem.Domain;
using AOGSystem.Domain.Attachments;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Persistence.EntityConfigurations.Attachmetns;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Persistence.Repository.Attachements
{
    public class AttachementRepository : IAttachementRepository
    {
        private readonly AOGSystemContext _context;
        public AttachementRepository(AOGSystemContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Attachment Add(Attachment attachement)
        {
            return _context.Attachments.Add(attachement).Entity;
        }

        public AttachmentLink Add(AttachmentLink attachement)
        {
            return _context.AttachmentLinks.Add(attachement).Entity;
        }

        public void DeleteAttachment(Guid id)
        {
            _context.Remove(_context.Attachments.FindAsync(id).Result);

        }

        public void DeleteAttachmentLink(Guid id)
        {
            _context.Remove(_context.AttachmentLinks.FindAsync(id).Result);
        }

        public async Task<PaginatedList<Attachment>> GetAllAttachments(Expression<Func<Attachment, bool>> predicate, int page, int pageSize)
        {
            IQueryable<Attachment> query = _context.Attachments;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            var result = await PaginatedList<Attachment>.ToPagedList(query.OrderByDescending(x => x.CreatedAT), page, pageSize);
            return result;
        }

        public async Task<List<Attachment>> GetAttachmentByFileNameAsync(string fileName)
        {
            return await _context.Attachments.Where(x => x.FileName == fileName).ToListAsync();
        }

        public async Task<Attachment> GetAttachmentByIDAsync(Guid? id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment != null)
            {
                _context.Entry(attachment);
            }
            return attachment;
        }

        public async Task<AttachmentLink> GetAttachmentLinkByAttachmentIdAsync(Guid attachmentId)
        {
            var attachment = await _context.AttachmentLinks.FindAsync(attachmentId);
            if (attachment != null)
            {
                _context.Entry(attachment);
            }
            return attachment;
        }

        public async Task<List<AttachmentLink>> GetAttachmentLinkByEntityIdAsync(Guid entityId, string entityType)
        {
            return await _context.AttachmentLinks.Where(x => x.EntityId == entityId &&  x.EntityType == entityType).Include(x => x.Attachment).ToListAsync();
        }

        public async Task<AttachmentLink> GetAttachmentLinkByIDAsync(Guid? id)
        {
            var attachment = await _context.AttachmentLinks.FindAsync(id);
            if (attachment != null)
            {
                _context.Entry(attachment);
            }
            return attachment;
        }

        public async Task<int> SaveChangesAsync(string userId = null, CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Attachment attachement)
        {
            _context.Entry(attachement).State = EntityState.Modified;
        }

        public void Update(AttachmentLink attachement)
        {
            _context.Entry(attachement).State = EntityState.Modified;
        }
    }
}
