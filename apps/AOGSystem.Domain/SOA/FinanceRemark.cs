using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.SOA
{
    public class FinanceRemark : BaseEntity
    {
        public string Message { get; private set; }

        public void SetMessage(string message) { Message = message; }

        public FinanceRemark(string message)
        {
            Message = message;
        }

    }

}
