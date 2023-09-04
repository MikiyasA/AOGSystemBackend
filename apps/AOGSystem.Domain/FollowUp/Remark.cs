using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.FollowUp
{
    public class Remark : BaseEntity
    {
        public int HomeBaseFollowUpId { get; private set; }
        public string? Message { get; private set; }

        public void SetHomebaseFollowUpId(int homeBaseFollowUpId) { this.HomeBaseFollowUpId= homeBaseFollowUpId; }
        public void SetMessage(string? message) { this.Message = message; }

        public Remark(int homeBaseFollowUpId, string? message)
        {
            this.SetMessage(message);
            this.SetHomebaseFollowUpId(homeBaseFollowUpId);
        }
    }
}
