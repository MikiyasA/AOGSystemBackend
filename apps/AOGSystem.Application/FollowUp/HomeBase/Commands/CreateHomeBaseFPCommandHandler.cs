using AOGSystem.Application.FollowUp.HomeBase.Query.Model;
using AOGSystem.Domain.FollowUp;
using AOGSystem.Domain.General;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.FollowUp.HomeBase.Commands
{
    public class CreateHomeBaseFPCommandHandler :
        IRequestHandler<CreateHomeBaseFPCommand, HomeBaseFollowUPQueryModel>
    {

        private readonly IHomeBaseFollowUpRepository _homeBaseFollowUpRepository;
        private readonly IPartRepository _partRepository;
        private readonly IMediator _mediator;
        public CreateHomeBaseFPCommandHandler(IHomeBaseFollowUpRepository homeBaseFollowUpRepository, 
            IPartRepository partRepository,
            IMediator mediator)
        {
            _homeBaseFollowUpRepository = homeBaseFollowUpRepository;
            _partRepository = partRepository;
            _mediator = mediator;
        }

        public async Task<HomeBaseFollowUPQueryModel> Handle(CreateHomeBaseFPCommand request, CancellationToken cancellationToken)
        {
            Part part = await _partRepository.GetPartByPNAsync(request.PartNumber);
            if (part == null)
            {
                part = new Part(request.PartNumber, request.Description, request.StockNo, request.FinancialClass);
            }

            var model = new HomeBaseFollowUp(
                request.RID,
                request.RequestDate,
                request.AirCraft,
                request.TailNo,
                request.Customer,
                part,
                request.PONumber,
                request.OrderType,
                request.Quantity,
                request.UOM,
                request.Vendor,
                request.ESD,
                request.NeedHigherMgntAttn);
            model.CreatedAT = DateTime.UtcNow;

            _homeBaseFollowUpRepository.Add(model);

            return new HomeBaseFollowUPQueryModel
            {
                Id = model.Id,
                RID = model.RID,
                RequestDate = model.RequestDate,
                AirCraft = model.AirCraft,
                TailNo = model.TailNo,
                Customer = model.Customer,
                Part = part,
                PONumber = model.PONumber,
                OrderType = model.OrderType,
                Quantity = model.Quantity,
                UOM = model.UOM,
                Vendor = model.Vendor,
                ESD = model.ESD,
                NeedHigherMgntAttn = model.NeedHigherMgntAttn,
            };
        }
    }

    public class CreateHomeBaseFPCommand : IRequest<HomeBaseFollowUPQueryModel>
    {
        public string? RID { get; set; } // Request ID
        public DateTime RequestDate { get; set; }
        public string? AirCraft { get; set; }
        public string? TailNo { get; set; }
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
        public string? ESD { get; set; } // Estimated Deliver Date
        public bool NeedHigherMgntAttn { get; set; }

        public CreateHomeBaseFPCommand() { }

        public CreateHomeBaseFPCommand(string? rID, DateTime requestDate, string? airCraft, string? tailNo, string? 
            customer, string? partNumber, string? description, string? stockNo, string? pONumber, string? orderType, 
            int quantity, string? uOM, string? vendor, string? eSD, bool needHigherMgntAttn) : this()
        {
            RID = rID;
            RequestDate = requestDate;
            AirCraft = airCraft;
            TailNo = tailNo;
            Customer = customer;
            PartNumber = partNumber;
            Description = description;
            StockNo = stockNo;
            PONumber = pONumber;
            OrderType = orderType;
            Quantity = quantity;
            UOM = uOM;
            Vendor = vendor;
            ESD = eSD;
            NeedHigherMgntAttn = needHigherMgntAttn;
        }
    }
}
