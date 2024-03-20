using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.SOA.Query;
using AOGSystem.Domain.SOA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.SOA.Commands
{
    public class AddBuyerRemarkCommandHandler : IRequestHandler<AddBuyerRemarkCommand, ReturnDto<RemarkQueryModel>>
    {
        private readonly IInvoiceListRepository _invoiceListRepository;
        public AddBuyerRemarkCommandHandler(IInvoiceListRepository invoiceListRepository)
        {
            _invoiceListRepository = invoiceListRepository;
        }
        public async Task<ReturnDto<RemarkQueryModel>> Handle(AddBuyerRemarkCommand request, CancellationToken cancellationToken)
        {
            var model = await _invoiceListRepository.GetSOAInvoiceListByIDAsync(request.InvoiceId);
            if (model == null)
                return new ReturnDto<RemarkQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Invoice could no be found",
                };
            model.AddBuyerRemark(request.Message, DateTime.Now, request.CreatedBy);
            _invoiceListRepository.Update(model);

            var result = await _invoiceListRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<RemarkQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when remark added",
                };

            var returnData = new RemarkQueryModel
            {
                InvoiceId = model.Id,
                InvoiceNo = model.InvoiceNo,
                Message = request.Message,
            };

            return new ReturnDto<RemarkQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Remark added successfully ",
            };
        }
    }

    public class AddBuyerRemarkCommand : IRequest<ReturnDto<RemarkQueryModel>>
    {
        public Guid InvoiceId { get; set; }
        public string Message { get; set; }


        [JsonIgnore]
        public Guid? CreatedBy { get; set; }
        public void SetCreatedBy(Guid createdBy) { CreatedBy = createdBy; }
    }
}
