using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class Remark : BaseEntity
    {
        public Guid AOGFollowUpId { get; private set; }
        public string? Message { get; private set; }

        public void SetAOGFollowUpId(Guid AOGFollowUpId) { this.AOGFollowUpId= AOGFollowUpId; }
        public void SetMessage(string? message) { this.Message = message; }

        public Remark(Guid AOGFollowUpId, string? message)
        {
            this.SetMessage(message);
            this.SetAOGFollowUpId(AOGFollowUpId);
        }
    }
}
