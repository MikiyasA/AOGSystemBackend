using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.CoreFollowUps
{
    public class CostSaving : BaseEntity
    {
        public string? OldPO { get; private set; }
        public string NewPO { get; private set; }
        public DateTime? IssueDate { get; private set; }
        public DateTime? CNDate { get; private set; }
        public decimal? OldPrice { get; private set; }
        public decimal? NewPrice { get; private set; }
        public decimal? PriceVariance { get; private set; }
        public int? Quantity { get; private set; }
        public decimal? SavingInUSD { get; private set; }
        public decimal? SavingInETB { get; private set; }
        public string? Remark { get; private set; }
        public bool? IsPurchaseOrder { get; private set; }
        public bool? IsRepairOrder { get; private set; }
        public string? SavedBy { get; private set; }
        public string? Status { get; private set; }


        public void SetOldPO(string oldPO) {  OldPO = oldPO; }
        public void SetNewPO(string newPO) { NewPO = newPO;}
        public void SetIssueDate(DateTime? issueDate) { IssueDate = issueDate; }
        public void SetCNDate(DateTime? cNDate) { CNDate = cNDate; }
        public void SetOldPrice(decimal? oldPrice) { OldPrice = oldPrice; }
        public void SetNewPrice(decimal? newPrice) {  NewPrice = newPrice; }
        public void SetPriceVariance(decimal? priceVariance) { PriceVariance = priceVariance; }
        public void SetQuantity(int? quantity) { Quantity = quantity; }
        public void SetSavingInUSD(decimal? savingInUSD) { SavingInUSD = savingInUSD; }
        public void SetSavingInETB(decimal? savingInETB) { SavingInETB = savingInETB; }
        public void SetRemart(string? remark) { Remark = remark; }
        public void SetIsPurchseOrder(bool isPurchase) { IsPurchaseOrder = isPurchase; }
        public void SetIsRepairOrder(bool isRepair) { IsRepairOrder = isRepair; }
        public void SetSavedBy(string savedBy) { SavedBy = savedBy;}
        public void SetStatus(string status) { Status = status; }

        public CostSaving() { }
        public CostSaving(string newPo) : this()
        {
            NewPO = newPo;
        }

        public CostSaving(string oldPo, string newPo, DateTime? issueDate, DateTime? cNDate, decimal? oldPrice, decimal? newPrice, decimal? priceVariance, int? quantity, decimal? savingInUSD,
            decimal? savingInETB, string? remark, bool isPurchase, bool isRepair, string savedBy, string status) : this()
        {
            OldPO = oldPo;
            NewPO = newPo;
            IssueDate = issueDate; 
            CNDate = cNDate;
            OldPrice = oldPrice;
            NewPrice = newPrice;
            PriceVariance = priceVariance;
            Quantity = quantity;
            SavingInUSD = savingInUSD;
            SavingInETB = savingInETB;
            Remark = remark;
            IsPurchaseOrder = isPurchase;
            IsRepairOrder = isRepair;
            SavedBy = savedBy;
            Status = status;

        }
    }
}
