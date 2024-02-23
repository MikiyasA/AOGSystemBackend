using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Loans.Query;
using AOGSystem.Domain.Loans;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Command
{
    public class AddOfferCommanHandler : IRequestHandler<AddOfferComman, ReturnDto<OfferQueryModel>>
    {
        private readonly ILoanPartListRepository _loanPartListRepository;
        public AddOfferCommanHandler(ILoanPartListRepository loanPartListRepository)
        {
            _loanPartListRepository = loanPartListRepository;
        }
        public async Task<ReturnDto<OfferQueryModel>> Handle(AddOfferComman request, CancellationToken cancellationToken)
        {
            var model = await _loanPartListRepository.GetLoanPartListByIDAsync(request.loanPartListId);
            if (model == null)
            {
                return new ReturnDto<OfferQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Loan Part List can not be found"
                };
            }
            var unitPrice = OrderUtility.GetLoanUnitPrice(request.Description, request.BasePrice, request.UnitPrice);
            var totalPrice = unitPrice * request.Quantity;
            var newOffer = new Offer(request.Description, request.BasePrice, request.Quantity, unitPrice, totalPrice, request.Currency);
            newOffer.CreatedAT = DateTime.Now;
            newOffer.CreatedBy = request.CreatedBy;

            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.CreatedBy;

            model.AddOffer(newOffer);

            _loanPartListRepository.Update(model);
            var result = await _loanPartListRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<OfferQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when offer added",
                };
            var returnData = new OfferQueryModel
            {
                Id = newOffer.Id,
                Description = newOffer.Description,
                BasePrice = newOffer.BasePrice,
                Quantity = newOffer.Quantity,
                UnitPrice = newOffer.UnitPrice,
                TotalPrice = newOffer.TotalPrice,
                Currency = newOffer.Currency,

            };
            return new ReturnDto<OfferQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Offer added successfully"
            };
        }
    }
    public class AddOfferComman : IRequest<ReturnDto<OfferQueryModel>>
    {
        public Guid loanPartListId { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; private set; }
        public void SetCreatedBy(string createdBy) { CreatedBy = createdBy; }
    }
}
