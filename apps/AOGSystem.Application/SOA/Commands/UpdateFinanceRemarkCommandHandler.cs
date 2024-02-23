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
    public class UpdateFinanceRemarkCommandHandler : IRequestHandler<UpdateFinanceRemarkCommand, ReturnDto<RemarkQueryModel>>
    {
        private readonly IInvoiceListRepository _invoiceListRepository;
        public UpdateFinanceRemarkCommandHandler(IInvoiceListRepository invoiceListRepository)
        {
            _invoiceListRepository = invoiceListRepository;
        }

        public async Task<ReturnDto<RemarkQueryModel>> Handle(UpdateFinanceRemarkCommand request, CancellationToken cancellationToken)
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
            model.UpdateFinanceRemark(request.Id, request.Message, DateTime.Now, request.UpdatedBy);
            _invoiceListRepository.Update(model);

            var result = await _invoiceListRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<RemarkQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when remark updated",
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
                Message = "Remark updated successfully ",
            };
        }
    }

    public class UpdateFinanceRemarkCommand : IRequest<ReturnDto<RemarkQueryModel>>
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string Message { get; set; }


        [JsonIgnore]
        public string? UpdatedBy { get; set; }
        public void SetUpdatedBy(string updatedBy) { UpdatedBy = updatedBy; }
    }
}
