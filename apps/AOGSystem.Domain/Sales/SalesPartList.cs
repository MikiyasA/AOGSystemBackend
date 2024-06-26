﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Sales
{
    public class SalesPartList : BaseEntity
    {
        public Guid PartId { get; private set; }
        public int Quantity { get; private set; }
        public string UOM { get; private set; }
        public double UnitPrice { get; private set; }
        public double TotalPrice { get; private set; }
        public string Currency { get; private set; }
        public string? RID { get; private set; }
        public string? SerialNo { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsInvoiced { get; private set; }

        public void SetPartId(Guid partId) { this.PartId = partId; }
        public void SetQuantity(int quantity) { this.Quantity = quantity; }
        public void SetUOM(string uOM) { this.UOM = uOM; }
        public void SetUnitPrice(double unitPrice) { this.UnitPrice = unitPrice; }
        public void SetTotalPrice(double totalPrice) { this.TotalPrice = totalPrice; }
        public void SetCurrency(string currency) { this.Currency = currency; }
        public void SetRID(string rid) { this.RID = rid; }
        public void SetSerialNo(string serialNo) { this.SerialNo = serialNo; }
        public void SetIsDeleted(bool isDeleted) { this.IsDeleted = isDeleted; }
        public void SetIsInvoiced(bool invoiced) {  IsInvoiced = invoiced; }

        public SalesPartList(Guid partId, int quantity, string uOM, double unitPrice, double totalPrice, string currency, string? rID, string? serialNo, bool isDeleted)
        {
            this.SetPartId(partId);
            this.SetQuantity(quantity);
            this.SetUOM(uOM);
            this.SetUnitPrice(unitPrice);
            this.SetTotalPrice(totalPrice);
            this.SetCurrency(currency);
            this.SetRID(rID);
            this.SetSerialNo(serialNo);
            this.SetIsDeleted(isDeleted);
        }
    }
}
