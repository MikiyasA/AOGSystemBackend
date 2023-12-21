using AOGSystem.Application.Quotations.Query;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Commands
{
    public class UpdatePartListInQuotationCommandHandler : IRequestHandler<UpdatePartListInQuotationCommand, QuotationPartListSummary>
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly IPartRepository _partRepository;
        public UpdatePartListInQuotationCommandHandler(IQuotationRepository quotationRepository,
            IPartRepository partRepository)
        {
            _quotationRepository = quotationRepository;
            _partRepository = partRepository;
        }

        public async Task<QuotationPartListSummary> Handle(UpdatePartListInQuotationCommand request, CancellationToken cancellationToken)
        {
            var model = await _quotationRepository.GetQuotationByIdAsync(request.QuotationId);
            var salesPrice = 0m;
            if(request.CurrentPrice < 50)
            {
                salesPrice = request.CurrentPrice + (request.CurrentPrice * 1.5m);
            } else if (request.CurrentPrice < 100)
            {
                salesPrice = request.CurrentPrice * 2;
            } else
            {
                salesPrice = request.CurrentPrice + (request.CurrentPrice * 0.5m);
            }
            var fixedLoanPrice = request.CurrentPrice * 0.065m;
            var loanPricePerDay = request.CurrentPrice * 0.01m;
            var exchangePrice = request.CurrentPrice * 0.1m;

            var part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if(part == null)
            {
                part = new Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass);
                part.UpdatedAT = DateTime.Now;
                _partRepository.Add(part);
                await _partRepository.SaveChangesAsync();
            }
            model.UpdateQuotationPartList(request.Id, part.Id, request.CurrentPrice, salesPrice, fixedLoanPrice, loanPricePerDay,
                exchangePrice, request.StockLocation, request.Condition, request.SerialNumber);

            //var newQPartList = new QuotationPartList(part.Id, request.CurrentPrice, salesPrice, fixedLoanPrice, loanPricePerDay, exchangePrice, request.StockLocation,
            //    request.Condition, request.SerialNumber);
            //newQPartList.CreatedAT= DateTime.Now;
            //model.AddQuotationPartList(newQPartList);
            _quotationRepository.Update(model);

            var result = await _quotationRepository.SaveChangesAsync();
            if (result == 0)
                return null;
            return new QuotationPartListSummary
            {
                Id = request.Id,
                PartId = part.Id,
                QuotationId = model.Id,
                CurrentPrice = request.CurrentPrice,
                SalesPrice = salesPrice,
                FixedLoanPrice = fixedLoanPrice,
                LoanPricePerDay = loanPricePerDay,
                ExchangePrice = exchangePrice,
                StockLocation = request.StockLocation,
                Condition = request.Condition,
                SerialNumber = request.SerialNumber,

            };
        }
    }
    public class UpdatePartListInQuotationCommand : IRequest<QuotationPartListSummary>
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? StockLocation { get; set; }
        public string? Condition { get; set; }
        public string? SerialNumber { get; set; }

        public UpdatePartListInQuotationCommand()
        {

        }
    }
}
