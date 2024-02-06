using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public class Offer : BaseEntity
    {
        public string Description { get; private set; }
        public double BasePrice { get; private set; }

        public int Quantity { get; private set; }
        public double UnitPrice { get; private set; }
        public double TotalPrice { get; private set; }
        public string Currency { get; private set; }

        public void SetDescription(string description) {  Description = description; }
        public void SetBasePrice(double basePrice) { BasePrice = basePrice; }
        public void SetQuantity(int quantity) { Quantity = quantity; }
        public void SetUnitPrice(double unitPrice) { UnitPrice = unitPrice;}
        public void SetTotalPrice(double totalPrice) { TotalPrice = totalPrice;}
        public void SetCurrency(string currency) { Currency = currency;}

        public Offer(string description, double basePrice, int quantity, double unitPrice, double totalPrice, string currency)
        {
            SetDescription(description);
            SetBasePrice(basePrice);
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
            SetTotalPrice(totalPrice);
            SetCurrency(currency);
        }

    }
}
