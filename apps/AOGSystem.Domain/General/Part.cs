using AOGSystem.Domain.Attachments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.General
{
    public class Part : BaseEntity
    {
        public string? PartNumber { get; private set; }
        public string? Description { get; private set;}
        public string? StockNo { get; private set; }
        public string? FinancialClass { get; private set; }
        public string? Manufacturer { get; private set; }
        public string? PartType { get; private set; }

        public void SetPartNumber(string? partNumber) { this.PartNumber = partNumber; }
        public void SetDescription(string? description) { this.Description = description; }
        public void SetStockNo(string stockNo) { this.StockNo = stockNo; }
        public void SetFinancialClass(string financialClass) { this.FinancialClass= financialClass; }
        public void SetManufacturer(string manufacturer) { this.Manufacturer = manufacturer; }
        public void SetPartType(string type) { this.PartType = type; }


        public Part(string partNumber, string description, string stockNo, string financialClass, string manufacturer, string partType)
        {
            this.SetPartNumber(partNumber);
            this.SetDescription(description);
            this.SetStockNo(stockNo);
            this.SetFinancialClass (financialClass);
            this.SetManufacturer(manufacturer);
            this.SetPartType(partType);
        }
    }
}
