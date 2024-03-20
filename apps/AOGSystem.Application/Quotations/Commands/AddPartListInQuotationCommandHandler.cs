using AOGSystem.Application.Quotations.Query;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Quotation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Quotations.Commands
{
    public class AddPartListInQuotationCommandHandler : IRequestHandler<AddPartListInQuotationCommand, QuotationPartListSummary>
    {
        private readonly IQuotationRepository _quotationRepository;
        private readonly IPartRepository _partRepository;
        public AddPartListInQuotationCommandHandler(IQuotationRepository quotationRepository,
            IPartRepository partRepository)
        {
            _quotationRepository = quotationRepository;
            _partRepository = partRepository;
        }

        public async Task<QuotationPartListSummary> Handle(AddPartListInQuotationCommand request, CancellationToken cancellationToken)
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
                part = new Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass, request.Manufacturer, request.PartType);
                part.CreatedAT = DateTime.Now;
                part.CreatedBy = request.CreatedBy;
                _partRepository.Add(part);
                await _partRepository.SaveChangesAsync();
            }

            var newQPartList = new QuotationPartList(part.Id, request.CurrentPrice, salesPrice, fixedLoanPrice, loanPricePerDay, exchangePrice, request.StockLocation,
                request.Condition, request.SerialNumber);
            newQPartList.CreatedAT= DateTime.Now;
            newQPartList.CreatedBy = request.CreatedBy;
            model.AddQuotationPartList(newQPartList);
            _quotationRepository.Update(model);

            var result = await _quotationRepository.SaveChangesAsync();
            if (result == 0)
                return null;
            return new QuotationPartListSummary
            {
                Id = newQPartList.Id,
                PartId = part.Id,
                QuotationId = model.Id,
                CurrentPrice = newQPartList.CurrentPrice,
                SalesPrice = newQPartList.SalesPrice,
                FixedLoanPrice = newQPartList.FixedLoanPrice,
                LoanPricePerDay = newQPartList.LoanPricePerDay,
                ExchangePrice = newQPartList.ExchangePrice,
                StockLocation = newQPartList.StockLocation,
                Condition = newQPartList.Condition,
                SerialNumber = newQPartList.SerialNumber,

            };
        }
    }
    public class AddPartListInQuotationCommand : IRequest<QuotationPartListSummary>
    {
        public Guid QuotationId { get; set; }
        public decimal CurrentPrice { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? StockLocation { get; set; }
        public string? Condition { get; set; }
        public string? SerialNumber { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartType { get; set; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }

        public AddPartListInQuotationCommand()
        {

        }
    }
}
