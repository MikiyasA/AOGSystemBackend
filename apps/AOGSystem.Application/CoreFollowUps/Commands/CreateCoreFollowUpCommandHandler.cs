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
    public class CreateCoreFollowUpCommandHandler : IRequestHandler<CreateCoreFollowUpCommand, ReturnDto<CoreFollowUpQueryModel>>
    {
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        public CreateCoreFollowUpCommandHandler(ICoreFollowUpRepository coreFollowUpRepository)
        {
            _coreFollowUpRepository = coreFollowUpRepository;
        }

        public async Task<ReturnDto<CoreFollowUpQueryModel>> Handle(CreateCoreFollowUpCommand request, CancellationToken cancellationToken)
        {
            var coreFP = await _coreFollowUpRepository.GetCoreFollowUpByPONoAsync(request.PONo);
            if(coreFP != null)
                return new ReturnDto<CoreFollowUpQueryModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Count = 1,
                    Message = "Core follow-up with this PO is already registered"
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
            var model = new CoreFollowUp(request.PONo, request.POCreatedDate ?? DateTime.Now, request.Aircraft, request.TailNo, request.PartNumber,
                request.Description, request.StockNo, request.Vendor, request.PartReleasedDate, request.PartReceiveDate, returnDueDate, request.ReturnProcessedDate,
                request.AWBNo, request.ReturnedPart, request.PODDate, request.Remark, request.Status);
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            _coreFollowUpRepository.Add(model);
            var result = await _coreFollowUpRepository.SaveChangesAsync(); 
            if(result == 0)
                return new ReturnDto<CoreFollowUpQueryModel>
                {
                    IsSuccess = false,
                    Data = null,
                    Count = 1,
                    Message = "Something went wrong when saving the core"
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
                Message = "Core follow-up registered successfully"
            };
        }
    }
    public class CreateCoreFollowUpCommand : IRequest<ReturnDto<CoreFollowUpQueryModel>>
    {
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
        public Guid? CreatedBy { get; private set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }


        public CreateCoreFollowUpCommand()
        {

        }
        public CreateCoreFollowUpCommand(string pONo, DateTime pOCreatedDate, string aircraft, string? tailNo, string? partNumber,
            string? description, string? stockNo, string? vendor, DateTime partReceiveDate, DateTime returnDueDate, 
            DateTime returnProcessedDate, string? aWBNo, string? returnedPart, DateTime pODDate, string? remark, string? status)
        {
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
