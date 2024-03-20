using AOGSystem.Application.CoreFollowUps.Query.Model.CostSaving;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.CostSavings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Commands.CostSaving
{
    public class UpdateCostSavingCommandHandler : IRequestHandler<UpdateCostSavingCommand, ReturnDto<CostSavingQueryModel>>
    {
        private readonly ICostSavingRepository _costSavingRepository;
        public UpdateCostSavingCommandHandler(ICostSavingRepository costSavingRepository)
        {
            _costSavingRepository = costSavingRepository;
        }

        public async Task<ReturnDto<CostSavingQueryModel>> Handle(UpdateCostSavingCommand request, CancellationToken cancellationToken)
        {
            var model =  await _costSavingRepository.GetCostSavingByIDAsync(request.Id);
            if (model == null)
                return new ReturnDto<CostSavingQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Cost Saving could no be found to update",
                };
            var priceVariace = request.OldPrice - request.NewPrice;
            var savingInUsd = priceVariace * request.Quantity;

            model.SetOldPO(request.OldPO);
            model.SetNewPO(request.NewPO);
            model.SetIssueDate(request.IssueDate);
            model.SetCNDate(request.CNDate);
            model.SetOldPrice(request.OldPrice);
            model.SetNewPrice(request.NewPrice);
            model.SetPriceVariance(priceVariace);
            model.SetQuantity(request.Quantity);
            model.SetSavingInUSD(savingInUsd);
            model.SetSavingInETB(request.SavingInETB);
            model.SetRemart(request.Remark);
            model.SetIsPurchseOrder(request.IsPurchaseOrder);
            model.SetIsRepairOrder(request.IsRepairOrder);
            model.SetSavedBy(request.SavedBy);
            model.SetStatus(request.Status);

            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdateBy;

            _costSavingRepository.Update(model);
            var result = await _costSavingRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<CostSavingQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when vendor created",
                };

            var returnData = new CostSavingQueryModel
            {
                Id = model.Id,
                OldPO = model.OldPO,
                NewPO = model.NewPO,
                IssueDate = model.IssueDate,
                CNDate = model.CNDate,
                OldPrice = model.OldPrice,
                NewPrice = model.NewPrice,
                PriceVariance = model.PriceVariance,
                Quantity = model.Quantity,
                SavingInUSD = model.SavingInUSD,
                SavingInETB = model.SavingInETB,
                Remark = model.Remark,
                IsPurchaseOrder = model.IsPurchaseOrder,
                IsRepairOrder = model.IsRepairOrder,
                SavedBy = model.SavedBy,
                Status = model.Status,
            };
            return new ReturnDto<CostSavingQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Cost saving successfully updated",
            };
        }
    }
    

    public class UpdateCostSavingCommand : IRequest<ReturnDto<CostSavingQueryModel>>
    {
        public Guid Id { get; set; }
        public string? OldPO { get; set; }
        public string NewPO { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? CNDate { get; set; }
        public decimal? OldPrice { get; set; }
        public decimal? NewPrice { get; set; }
        public decimal? PriceVariance { get; set; }
        public int? Quantity { get; set; }
        public decimal? SavingInUSD { get; set; }
        public decimal? SavingInETB { get; set; }
        public string? Remark { get; set; }
        public bool IsPurchaseOrder { get; set; }
        public bool IsRepairOrder { get; set; }
        public string? SavedBy { get; set; }
        public string? Status { get; set; }

        [JsonIgnore]
        public Guid? UpdateBy { get; private set; }
        public void SetUpdateBy(Guid updateBy) { UpdateBy = updateBy; }

    }
}
