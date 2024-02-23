using AOGSystem.Domain.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Attachments.Query
{
    public class AttachmentQueryModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
        public Guid AttachmentId { get; set; }
        public Guid AttachmentLinkId { get; set; }
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
    }
}
