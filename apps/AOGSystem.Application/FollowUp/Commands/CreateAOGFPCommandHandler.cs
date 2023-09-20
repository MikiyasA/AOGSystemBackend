using AOGSystem.Application.FollowUp.Query.Model;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.Commands
{
    public class CreateAOGFPCommandHandler :
        IRequestHandler<CreateAOGFPCommand, AOGFollowUPQueryModel>
    {

        private readonly IAOGFollowUpRepository _AOGFollowUpRepository;
        private readonly IPartRepository _partRepository;
        private readonly IMediator _mediator;
        public CreateAOGFPCommandHandler(IAOGFollowUpRepository AOGFollowUpRepository,
            IPartRepository partRepository,
            IMediator mediator)
        {
            _AOGFollowUpRepository = AOGFollowUpRepository;
            _partRepository = partRepository;
            _mediator = mediator;
        }

        public async Task<AOGFollowUPQueryModel> Handle(CreateAOGFPCommand request, CancellationToken cancellationToken)
        {

            var part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if (part == null)
            {
                part = new Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass);
                part.CreatedAT = DateTime.UtcNow;
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
                    part.Id,
                    request.PONumber,
                    request.OrderType,
                    request.Quantity,
                    request.UOM,
                    request.Vendor,
                    request.EDD,
                    request.Status,
                    request.AWBNo,
                    request.NeedHigherMgntAttn);
            model.CreatedAT = DateTime.UtcNow;

            _AOGFollowUpRepository.Add(model);

            var result = await _AOGFollowUpRepository.SaveChangesAsync();

            if (result == 0)
                return null;

            return new AOGFollowUPQueryModel
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
            };
        }
    }

    public class CreateAOGFPCommand : IRequest<AOGFollowUPQueryModel>
    {
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
        public bool NeedHigherMgntAttn { get; set; }

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
