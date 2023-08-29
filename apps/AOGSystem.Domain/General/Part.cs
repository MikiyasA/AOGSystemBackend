﻿using System;
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
        public string? FinancialClass { get; private set; }

        public void SetPartNumber(string? partNumber) { this.PartNumber = partNumber; }
        public void SetDescription(string? description) { this.Description = description; }
        public void SetFinancialClass(string financialClass) { this.FinancialClass= financialClass; }

        public Part(string partNumber, string description, string financialClass)
        {
            this.SetPartNumber(partNumber);
            this.SetDescription(description);
            this.SetFinancialClass (financialClass);
        }
    }
}