using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application
{
    public static class OrderUtility
    {
        public static int GetNextOrderNo(string orderNumber)
        {
            string numericPart = new string(orderNumber.Where(char.IsDigit).ToArray());
            var orderSequence = numericPart.Substring(4); // Exclude the 'S' prefix
            int orderNo = int.Parse(orderSequence);
            return orderNo + 1;
        }

        public static double GetLoanUnitPrice(string description, double basePrice, double? price)
        {
            var unitPrice = 0.0;
            if (description == "Availability charge (One time)")
            {
                unitPrice = basePrice * 0.065;
            }
            else if (description == "Loan charge (from 1st - 10th Day)")
            {
                unitPrice = basePrice * 0.01;
            }
            else if (description == "Loan charge (from 11th - 30th Day)")
            {
                unitPrice = basePrice * 0.02;
            }
            else if (description == "Loan charge (from 31st - 45th Day)")
            {
                unitPrice = basePrice * 0.03;
            } else
            {
                unitPrice = (double)price;
            }
            return unitPrice;
        }
    }
}
