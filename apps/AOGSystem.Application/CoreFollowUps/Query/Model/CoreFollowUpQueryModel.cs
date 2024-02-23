using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Query.Model
{
    public class CoreFollowUpSummary
    {
        public Guid Id { get; set; }
        public string PONo { get; set; }
        public DateTime POCreatedDate { get; set; }
        public string Aircraft { get; set; }
        public string? TailNo { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? Vendor { get; set; }
        public DateTime? PartReceiveDate { get; set; }
        public DateTime ReturnDueDate { get; set; }
        public DateTime? ReturnProcessedDate { get; set; }
        public string? AWBNo { get; set; }
        public string? ReturnedPart { get; set; }
        public DateTime? PODDate { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }

        //internal static CoreFollowUp ToModel(CoreFollowUpSummary item)
        //{
        //    return new CoreFollowUp(
        //        item.PONo
        //        ....)
        //}
        //internal static List<CoreFollowUp> ToModel(List<CoreFollowUpSummary> lists)
        //{
        //    return lists.Select(x => ToModel(x)).ToList();
        //}

    }
    public class CoreFollowUpQueryModel : CoreFollowUpSummary
    {
    }
}
