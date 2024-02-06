using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.Invoice.Query.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Invoice.Query
{
    public interface IInvoiceQuery
    {
        Task<List<ActiveInvoicesQueryModel>> GetAllActiveInvoicesAsync();
    }
}
