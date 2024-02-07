using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.CoreFollowUps
{
    public class CoreFollowUp : BaseEntity
    {
        public const string ORDER_TYPE_EXCHANGE = "exchange";

        public const string STATUS_TRANSACTION_CHANGED = "transactionChanged";
        public const string STATUS_BACK_TO_EXCHANGE = "backToExchange";

        public string PONo { get; private set; }
        public DateTime POCreatedDate { get; private set; }
        public string Aircraft { get; private set; }
        public string? TailNo { get; private set; }
        public string? PartNumber { get; private set; }
        public string? Description { get; private set; }
        public string? StockNo { get; private set; }
        public string? Vendor { get; private set; }
        public DateTime? PartReleasedDate { get; private set; }
        public DateTime? PartReceiveDate { get; private set; }
        public DateTime ReturnDueDate { get; private set; }
        public DateTime? ReturnProcessedDate { get; private set; }
        public string? AWBNo { get; private set; }
        public string? ReturnedPart { get; private set; }
        public DateTime? PODDate { get; private set; }
        public string? Remark { get; private set; }
        public string? Status { get; private set; }


        public void SetPONo(string pONo) { this.PONo = pONo; }  
        public void SetPOCreatedDate(DateTime pOCreatedDate) { this.POCreatedDate = pOCreatedDate;  }
        public void SetAircraft(string aircraft) { this.Aircraft = aircraft; }
        public void SetTailNo(string tailNo) { this.TailNo = tailNo; }
        public void SetPartNumber(string partNumber) { this.PartNumber = partNumber; }
        public void SetDescription(string description) { this.Description = description; }
        public void SetStockNo(string stockNo) { this.StockNo = stockNo; }
        public void SetVendor(string vendor) { this.Vendor = vendor; }
        public void SetPartReleasedDate(DateTime? partReleasedDate) { PartReleasedDate = partReleasedDate;  }
        public void SetPartReceiveDate(DateTime? partReceiveDate) { this.PartReceiveDate = partReceiveDate;}
        public void SetReturnDueDate(DateTime returnDueDate) { this.ReturnDueDate = returnDueDate;}
        public void SetReturnProcessedDate(DateTime? returnProcessedDate) { this.ReturnProcessedDate = returnProcessedDate;}
        public void SetAWBNo(string? aWBNo) { this.AWBNo = aWBNo;}
        public void SetReturnedPart(string? returnedPart) { this.ReturnedPart = returnedPart; }
        public void SetPODDate(DateTime? pODDate) { this.PODDate = pODDate;}
        public void SetRemarK(string? remark) { this.Remark = remark; }
        public void SetStatus(string? status) { this.Status = status; }


        public CoreFollowUp()
        {

        }
        public CoreFollowUp(string pONo, DateTime pOCreatedDate, string aircraft, string tailNo, string partNumber, string description, string stockNo,
            string? vendor, DateTime? partReleasedDate, DateTime? partReceiveDate, DateTime returnDueDate, DateTime? returnProcessedDate, string? aWBNo, string? returnedPart,
            DateTime? pODDate, string? remark, string status)
        {
            SetPONo(pONo);
            SetPOCreatedDate(pOCreatedDate);
            SetAircraft(aircraft);
            SetTailNo(tailNo);
            SetPartNumber(partNumber);
            SetDescription(description);
            SetStockNo(stockNo);
            SetVendor(vendor);
            SetPartReleasedDate(partReleasedDate);
            SetPartReceiveDate(partReceiveDate);
            SetReturnDueDate(returnDueDate);
            SetReturnProcessedDate(returnProcessedDate);
            SetAWBNo(aWBNo);
            SetReturnedPart(returnedPart);
            SetPODDate(pODDate);
            SetRemarK(remark);
            SetStatus(status);
        }

        public CoreFollowUp(string pONo, DateTime pOCreatedDate, string aircraft, string tailNo, string partNumber, string description, string stockNo,
            string? vendor, DateTime returnDueDate)
        {
            SetPONo(pONo);
            SetPOCreatedDate(pOCreatedDate);
            SetAircraft(aircraft);
            SetTailNo(tailNo);
            SetPartNumber(partNumber);
            SetDescription(description);
            SetStockNo(stockNo);
            SetVendor(vendor);
            SetReturnDueDate(returnDueDate);
        }

    }
}
