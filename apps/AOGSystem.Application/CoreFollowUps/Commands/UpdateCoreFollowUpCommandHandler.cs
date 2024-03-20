using AOGSystem.Application.CoreFollowUps.Query.Model;
using AOGSystem.Application.General.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Commands
{
    public class UpdateCoreFollowUpCommandHandler : IRequestHandler<UpdateCoreFollowUpCommand, ReturnDto<CoreFollowUpQueryModel>>
    {
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        public UpdateCoreFollowUpCommandHandler(ICoreFollowUpRepository coreFollowUpRepository)
        {
            _coreFollowUpRepository = coreFollowUpRepository;
        }

        public async Task<ReturnDto<CoreFollowUpQueryModel>> Handle(UpdateCoreFollowUpCommand request, CancellationToken cancellationToken)
        {
            var model = await _coreFollowUpRepository.GetCoreFollowUpByIDAsync(request.Id);
            if(model == null)
                return new ReturnDto<CoreFollowUpQueryModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Count = 1,
                    Message = "Core follow-up can not be found to update"
                };
            DateTime returnDueDate;
            if (request.ReturnDueDate == null)
            {
                if (request.PartReleasedDate != null)
                    returnDueDate = request.PartReleasedDate.Value.AddDays(10);
                else
                    returnDueDate = DateTime.Now.AddDays(10);
            }
            else
            {
                returnDueDate = (DateTime)request.ReturnDueDate;
            }
            model.SetPONo(request.PONo);
            model.SetPOCreatedDate((DateTime)request.POCreatedDate);
            model.SetAircraft(request.Aircraft);
            model.SetTailNo(request.TailNo);
            model.SetPartNumber(request.PartNumber);
            model.SetDescription(request.Description);
            model.SetStockNo(request.StockNo);
            model.SetVendor(request.Vendor);
            model.SetPartReleasedDate(request.PartReleasedDate);
            model.SetPartReceiveDate(request.PartReceiveDate);
            model.SetReturnDueDate(returnDueDate);
            model.SetReturnProcessedDate(request.ReturnProcessedDate);
            model.SetAWBNo(request.AWBNo);
            model.SetReturnedPart(request.ReturnedPart);
            model.SetPODDate(request.PODDate);
            model.SetRemarK(request.Remark);
            model.SetStatus(request.Status);
            model.UpdatedAT = DateTime.Now;
            model.UpdatedBy = request.UpdatedBy;

            _coreFollowUpRepository.Update(model);
            var result = await _coreFollowUpRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<CoreFollowUpQueryModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Count = 1,
                    Message = "Something went wrong when updating the core"
                };
            var returnData = new CoreFollowUpQueryModel
            {
                Id = model.Id,
                PONo = model.PONo,
                POCreatedDate = model.POCreatedDate,
                Aircraft = model.Aircraft,
                TailNo = model.TailNo,
                PartNumber = model.PartNumber,
                Description = model.Description,
                StockNo = model.StockNo,
                Vendor = model.Vendor,
                PartReceiveDate = model.PartReceiveDate,
                ReturnDueDate = model.ReturnDueDate,
                ReturnProcessedDate = model.ReturnProcessedDate,
                AWBNo = model.AWBNo,
                ReturnedPart = model.ReturnedPart,
                PODDate = model.PODDate,
                Remark = model.Remark,
                Status = model.Status
            };
            return new ReturnDto<CoreFollowUpQueryModel>
            {
                IsSuccess = true,
                Data = returnData,
                Count = 1,
                Message = "Core follow-up updated successfully"
            };
        }
    }
    public class UpdateCoreFollowUpCommand : IRequest<ReturnDto<CoreFollowUpQueryModel>>
    {
        public Guid Id { get; set; }
        public string PONo { get; set; }
        public DateTime? POCreatedDate { get; set; }
        public string Aircraft { get; set; }
        public string? TailNo { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? Vendor { get; set; }
        public DateTime? PartReleasedDate { get; set; }
        public DateTime? PartReceiveDate { get; set; }
        public DateTime? ReturnDueDate { get; set; }
        public DateTime? ReturnProcessedDate { get; set; }
        public string? AWBNo { get; set; }
        public string? ReturnedPart { get; set; }
        public DateTime? PODDate { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }

        [JsonIgnore]
        public Guid? UpdatedBy { get; private set; }
        public void SetUpdatedBy(Guid updatedBy) { UpdatedBy = updatedBy; }


        public UpdateCoreFollowUpCommand()
        {

        }
        public UpdateCoreFollowUpCommand(Guid id, string pONo, DateTime pOCreatedDate, string aircraft, string? tailNo, string? partNumber,
            string? description, string? stockNo, string? vendor, DateTime? partReceiveDate, DateTime returnDueDate,
            DateTime? returnProcessedDate, string? aWBNo, string? returnedPart, DateTime? pODDate, string? remark, string? status)
        {
            Id = id; 
            PONo = pONo;
            POCreatedDate = pOCreatedDate;
            Aircraft = aircraft;
            TailNo = tailNo;
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            Vendor = vendor;
            PartReceiveDate = partReceiveDate;
            ReturnDueDate = returnDueDate;
            ReturnProcessedDate = returnProcessedDate;
            AWBNo = aWBNo;
            ReturnedPart = returnedPart;
            PODDate = pODDate;
            Remark = remark;
            Status = status;
        }
    }
}
