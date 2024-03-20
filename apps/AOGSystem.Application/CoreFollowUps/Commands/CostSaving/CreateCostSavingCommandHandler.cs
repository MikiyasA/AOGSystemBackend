using AOGSystem.Application.CoreFollowUps.Query.Model.CostSaving;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.CoreFollowUps;
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
    public class CreateCostSavingCommandHandler : IRequestHandler<CreateCostSavingCommand, ReturnDto<CostSavingQueryModel>>
    {
        private readonly ICostSavingRepository _costSavingRepository;
        public CreateCostSavingCommandHandler(ICostSavingRepository costSavingRepository)
        {
            _costSavingRepository = costSavingRepository;
        }

        public async Task<ReturnDto<CostSavingQueryModel>> Handle(CreateCostSavingCommand request, CancellationToken cancellationToken)
        {
            var priceVariace = request.OldPrice - request.NewPrice;
            var savingInUsd = priceVariace * request.Quantity;
            var model = new Domain.CoreFollowUps.CostSaving(request.OldPO, request.NewPO, request.IssueDate, request.CNDate, request.OldPrice, request.NewPrice, priceVariace, request.Quantity,
                savingInUsd, request.SavingInETB, request.Remark, request.IsPurchaseOrder, request.IsRepairOrder, request.SavedBy, request.Status);

            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _costSavingRepository.Add(model);
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
                Message = "Cost saving successfully created",
            };
        }
    }

    public class CreateCostSavingCommand : IRequest<ReturnDto<CostSavingQueryModel>>
    {

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
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
