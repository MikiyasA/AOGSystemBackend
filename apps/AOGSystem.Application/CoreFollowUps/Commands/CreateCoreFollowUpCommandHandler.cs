using AOGSystem.Application.CoreFollowUps.Query.Model;
using AOGSystem.Domain.CoreFollowUps;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.CoreFollowUps.Commands
{
    public class CreateCoreFollowUpCommandHandler : IRequestHandler<CreateCoreFollowUpCommand, CoreFollowUpQueryModel>
    {
        private readonly ICoreFollowUpRepository _coreFollowUpRepository;
        public CreateCoreFollowUpCommandHandler(ICoreFollowUpRepository coreFollowUpRepository)
        {
            _coreFollowUpRepository = coreFollowUpRepository;
        }

        public async Task<CoreFollowUpQueryModel> Handle(CreateCoreFollowUpCommand request, CancellationToken cancellationToken)
        {
            var coreFP = await _coreFollowUpRepository.GetCoreFollowUpByPONoAsync(request.PONo);
            if(coreFP != null)
            {
                // TODO
                return null;
            }
            
            var model = new CoreFollowUp(request.PONo, request.POCreatedDate, request.Aircraft, request.TailNo, request.PartNumber,
                request.Description, request.StockNo, request.Vendor, request.PartReceiveDate, request.ReturnDueDate, request.ReturnProcessedDate,
                request.AWBNo, request.ReturnedPart, request.PODDate, request.Remark, request.Status);
            model.CreatedAT = DateTime.UtcNow;
            _coreFollowUpRepository.Add(model);
            var result = await _coreFollowUpRepository.SaveChangesAsync();
            if(result == 0)
            {
                return null;
            }

            return new CoreFollowUpQueryModel
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
        }
    }
    public class CreateCoreFollowUpCommand : IRequest<CoreFollowUpQueryModel>
    {
        public string PONo { get; set; }
        public DateTime POCreatedDate { get; set; }
        public string Aircraft { get; set; }
        public string? TailNo { get; set; }
        public string? PartNumber { get; set; }
        public string? Description { get; set; }
        public string? StockNo { get; set; }
        public string? Vendor { get; set; }
        public DateTime PartReceiveDate { get; set; }
        public DateTime ReturnDueDate { get; set; }
        public DateTime ReturnProcessedDate { get; set; }
        public string? AWBNo { get; set; }
        public string? ReturnedPart { get; set; }
        public DateTime PODDate { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }

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
