using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Loans.Query;
using AOGSystem.Domain.Loans;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Command
{
    public class UpdateOfferCommanHandler : IRequestHandler<UpdateOfferCommand, ReturnDto<OfferQueryModel>>
    {
        private readonly IOfferRepository _offerRepository;
        public UpdateOfferCommanHandler(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public async Task<ReturnDto<OfferQueryModel>> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var model = await _offerRepository.GetOfferByIDAsync(request.Id);
            if (model == null)
            {
                return new ReturnDto<OfferQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Offer can not be found"
                };
            }
            var totalPrice = request.Quantity * request.UnitPrice;
            model.SetDescription(request.Description);
            model.SetBasePrice(request.BasePrice);
            model.SetQuantity(request.Quantity);
            model.SetUnitPrice(request.UnitPrice);
            model.SetTotalPrice(totalPrice);
            model.SetCurrency(request.Currency);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.UpdatedBy;

            _offerRepository.Update(model);
            var result = await _offerRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<OfferQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when offer updated",
                };
            var returnData = new OfferQueryModel
            {
                Id = model.Id,
                Description = model.Description,
                BasePrice = model.BasePrice,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
                Currency = model.Currency,
            };
            return new ReturnDto<OfferQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Offer updated successfully"
            };

        }
    }
    public class UpdateOfferCommand : IRequest<ReturnDto<OfferQueryModel>>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; }

        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Currency { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; private set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
