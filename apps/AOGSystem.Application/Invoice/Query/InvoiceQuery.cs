using AOGSystem.Application.Invoice.Query.Model;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Invoices;
using AOGSystem.Domain.Loans;
using AOGSystem.Domain.Sales;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Invoice.Query
{
    public class InvoiceQuery : IInvoiceQuery
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ISaleRepository _saleRepository;
        public InvoiceQuery(IInvoiceRepository invoiceRepository, ICompanyRepository companyRepository, ILoanRepository loanRepository, ISaleRepository saleRepository)
        {
            _invoiceRepository = invoiceRepository;
            _companyRepository = companyRepository;
            _loanRepository = loanRepository;
            _saleRepository = saleRepository;
        }
        public async Task<List<ActiveInvoicesQueryModel>> GetAllActiveInvoicesAsync()
        {
            var returnInvoices = new List<ActiveInvoicesQueryModel>();
            var invoices = await _invoiceRepository.GetActiveInvoices();

            returnInvoices = invoices.Select(async inv =>
            {
                var loanOrder = await _loanRepository.GetLoanByIDAsync(inv.LoanOrderId);
                var salesOrder = await _saleRepository.GetSalesByIDAsync(inv.SalesOrderId);
                var companyLoan = await _companyRepository.GetCompanyByIDAsync(loanOrder?.CompanyId);
                var companySales = await _companyRepository.GetCompanyByIDAsync(salesOrder?.CompanyId);

                return new ActiveInvoicesQueryModel
                {
                    Id = inv.Id,
                    InvoiceNo = inv.InvoiceNo,
                    InvoiceDate = inv.InvoiceDate,
                    DueDate = inv.DueDate,
                    SalesOrder = salesOrder,
                    LoanOrder = loanOrder,
                    Company = inv.TransactionType == "Sales" ? companySales : inv.TransactionType == "Loan" ? companyLoan : null,
                    InvoicePartLists = inv.InvoicePartLists,
                    TransactionType = inv.TransactionType,
                    IsApproved = inv.IsApproved,
                    POPReference = inv.POPReference,
                    POPDate = inv.POPDate,
                    Status = inv.Status,
                    Remark = inv.Remark,
                };
            }).Select(t => t.Result).ToList();

            return returnInvoices;
        }
    }
}
