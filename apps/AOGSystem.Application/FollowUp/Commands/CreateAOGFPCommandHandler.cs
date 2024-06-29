using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using AOGSystem.Domain.CostSavings;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using Kros.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class CreateAOGFPCommandHandler :
        IRequestHandler<CreateAOGFPCommand, ReturnDto<AOGFollowUPQueryModel>>
    {

        private readonly IFollowUpTabsRepository _followUpTabsRepository;
        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        private readonly IPartRepository _partRepository;
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        private readonly ICostSavingRepository _costSavingRepository;
        private readonly IMediator _mediator;
        public CreateAOGFPCommandHandler(IFollowUpTabsRepository followUpTabsRepository,
            IAOGFollowUpRepository AOGFollowUpRepository,
            IPartRepository partRepository,
            ICoreFollowUpRepository coreFollowUpRepository,
            ICostSavingRepository costSavingRepository,
            IMediator mediator)
        {
            _followUpTabsRepository = followUpTabsRepository;
            _AOGFollowUpRepository = AOGFollowUpRepository;
            _partRepository = partRepository;
            _coreFollowUpRepository = coreFollowUpRepository;
            _costSavingRepository = costSavingRepository;
            _mediator = mediator;
        }

        public async Task<ReturnDto<AOGFollowUPQueryModel>> Handle(CreateAOGFPCommand request, CancellationToken cancellationToken)
        {

            var tab = await _followUpTabsRepository.GetFollowUpTabsByIDAsync(request.FollowUpTabsId);
            if (tab == null)
                return new ReturnDto<AOGFollowUPQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Tab cannot be found. Please check if you are updating existed tab"

                };
            var part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if (part == null && !request.PartNumber.IsNullOrEmpty())
            {
                part = new Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass, request.Manufacturer, request.PartType);
                part.CreatedAT = DateTime.Now;
                part.CreatedBy = request.CreatedBy;
                _partRepository.Add(part);
                await _partRepository.SaveChangesAsync();
            }

            var model = new AOGFollowUp(
                    request.RID,
                    request.RequestDate,
                    request.AirCraft,
                    request.TailNo,
                    request.WorkLocation,
                    request.AOGStation,
                    request.Customer,
                    part?.Id,
                    request.PONumber,
                    request.OrderType,
                    request.Quantity,
                    request.UOM,
                    request.Vendor,
                    request.EDD,
                    request.Status,
                    request.AWBNo,
                    request.FlightNo,
                    request.NeedHigherMgntAttn);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            #region Core follow up logic 
            var coreFPExists = await _coreFollowUpRepository.GetCoreFollowUpByPONoAsync(model.PONumber);
            if (coreFPExists == null)
            {
                if (model.OrderType.ToLower() == CoreFollowUp.ORDER_TYPE_EXCHANGE.ToLower())
                {
                    var returnDueDate = DateTime.Now.AddDays(10);
                    var coreFollowup = new CoreFollowUp(model.PONumber, DateTime.Now, model.AirCraft, model.TailNo, part.PartNumber,
                        part.Description, part.StockNo, model.Vendor, returnDueDate);
                    coreFollowup.CreatedAT = DateTime.Now;
                    coreFollowup.CreatedBy = request.CreatedBy;
                    _coreFollowUpRepository.Add(coreFollowup);
                    await _coreFollowUpRepository.SaveChangesAsync();
                }

            }
            else
            {
                if (model.OrderType == CoreFollowUp.ORDER_TYPE_EXCHANGE)
                {
                    coreFPExists.SetPONo(model.PONumber);
                    coreFPExists.SetAircraft(model.AirCraft);
                    coreFPExists.SetTailNo(model.TailNo);
                    coreFPExists.SetPartNumber(request.PartNumber);
                    coreFPExists.SetDescription(request.Description);
                    coreFPExists.SetStockNo(request.StockNo);
                    coreFPExists.SetVendor(model.Vendor);
                    coreFPExists.UpdatedAT = DateTime.Now;
                    _coreFollowUpRepository.Update(coreFPExists);
                    var r = await _coreFollowUpRepository.SaveChangesAsync();
                }
                else
                {
                    coreFPExists.SetStatus(CoreFollowUp.STATUS_TRANSACTION_CHANGED);
                    _coreFollowUpRepository.Update(coreFPExists);
                }
            }
            #endregion

            if (request.HaveCostSaving)
            {
                var CSExist = await _costSavingRepository.GetCostSavingByNewPONoAsync(request.PONumber);
                if (CSExist == null)
                    _costSavingRepository.Add(new CostSaving(request.PONumber));
            }

            var newRemark = new Remark(model.Id, request.Message);
            newRemark.CreatedAT = DateTime.Now;
            newRemark.CreatedBy = request.CreatedBy;
            model.AddRemark(newRemark);

            tab.AddFollowUp(model);

            _followUpTabsRepository.Update(tab);

            var result = await _followUpTabsRepository.SaveChangesAsync();

            if (result == 0)
                return null;

            var returnData = new AOGFollowUPQueryModel
            {
                Id = model.Id,
                RID = model.RID,
                RequestDate = model.RequestDate,
                AirCraft = model.AirCraft,
                TailNo = model.TailNo,
                WorkLocation = model.WorkLocation,
                AOGStation = model.AOGStation,
                Customer = model.Customer,
                PartId = part.Id,
                PONumber = model.PONumber,
                OrderType = model.OrderType,
                Quantity = model.Quantity,
                UOM = model.UOM,
                Vendor = model.Vendor,
                EDD = model.EDD,
                Status = model.Status,
                AWBNo = model.AWBNo,
                NeedHigherMgntAttn = model.NeedHigherMgntAttn,
                Remark = newRemark
            };

            return new ReturnDto<AOGFollowUPQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "AOG follow up created successfully"
            };
        }
    }

    public class CreateAOGFPCommand : IRequest<ReturnDto<AOGFollowUPQueryModel>>
    {
        public Guid FollowUpTabsId { get; set; }

        public string? RID { get; set; } // Request ID
        public DateTime RequestDate { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
        public string? WorkLocation { get; set; }
        public string? AOGStation { get; set; }

        public string? Customer { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? FinancialClass { get; set; }
        public string? PONumber { get; set; } // purchase order number 
        public string? OrderType { get; set; }
        public int Quantity { get; set; }
        public string? UOM { get; set; } // unit of measurement
        public string? Vendor { get; set; }
        public DateTime? EDD { get; set; } // Estimated Deliver Date
        public string? Status { get; set; }
        public string? AWBNo { get; set; }
        public string FlightNo { get; set; }
        public bool NeedHigherMgntAttn { get; set; }
        public string Message { get; set; }
        public string? Manufacturer { get; set; }
        public string? PartType { get; set; }
        public bool HaveCostSaving { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }

        [JsonIgnore]
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }

        public CreateAOGFPCommand() { }

        public CreateAOGFPCommand(string? rID, DateTime requestDate, string? airCraft, string? tailNo, string? workLocation, string? airStation,
            string? customer, string? partNumber, string? description, string? stockNo, string? pONumber, string? orderType,
            int quantity, string? uOM, string? vendor, DateTime? eDD, string status, string awbNo, bool needHigherMgntAttn) : this()
        {
            RID = rID;
            RequestDate = requestDate;
            AirCraft = airCraft;
            TailNo = tailNo;
            WorkLocation = workLocation;
            AOGStation = airStation;
            Customer = customer;
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            PONumber = pONumber;
            OrderType = orderType;
            Quantity = quantity;
            UOM = uOM;
            Vendor = vendor;
            EDD = eDD;
            Status = status;
            AWBNo = awbNo;
            NeedHigherMgntAttn = needHigherMgntAttn;
        }
    }
}
