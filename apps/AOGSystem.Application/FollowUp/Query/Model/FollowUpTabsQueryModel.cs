using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Query.Model
{
    public class FollowUpTabsSummery
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public string? Status { get; set; }

        public static FollowUpTabs ToModel(FollowUpTabsSummery item)
        {
            return new FollowUpTabs(
                item.Name,
                item.Color,
                item.Status);
        }
        internal static List<FollowUpTabs> ToModel(List<FollowUpTabsSummery> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();
        }
    }
    public class FollowUpTabsQueryModel : FollowUpTabsSummery
    {
        public List<RemarkSummery> Remarks { get; set; } = new List<RemarkSummery>();
    }
}
