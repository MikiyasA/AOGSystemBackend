using AOGSystem.Application.General.Query.Model;
using AOGSystem.Application.Loans.Query;
using AOGSystem.Domain.General;
using AOGSystem.Domain.Loans;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Wordprocessing;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AOGSystem.Application.Loans.Command
{
    public class AddLoanPartListCommanHandler : IRequestHandler<AddLoanPartListComman, ReturnDto<LoanPartListQueryModel>>
    {
        private readonly ILoanRepository _loanRepository;
        public AddLoanPartListCommanHandler(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<ReturnDto<LoanPartListQueryModel>> Handle(AddLoanPartListComman request, CancellationToken cancellationToken)
        { 
            var model = await _loanRepository.GetLoanByIDAsync(request.LoanId);
            if (model == null)
            {
                return new ReturnDto<LoanPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "The Loan order can not be found"
                };
            }
            var newPartList = new LoanPartList(request.PartId, request.Quantity, request.UOM);
            newPartList.CreatedAT = DateTime.Now;
            newPartList.CreatedBy = request.CreatedBy;

            if(request.Description != null)
            {
                foreach (var description in request.Description)
                {
                    var unitPrice = OrderUtility.GetLoanUnitPrice(description, request.BasePrice, request.UnitPrice);
                    var totalPrice = unitPrice * request.Quantity;
                    var newOffer = new Offer(description, request.BasePrice, request.Quantity, unitPrice, totalPrice, request.Currency);
                    newOffer.CreatedAT = DateTime.Now;
                    newOffer.CreatedBy = request.CreatedBy;
                    newPartList.AddOffer(newOffer);
                }
            }
            //else
            //{
            //    var unitPrice = OrderUtility.GetLoanUnitPrice("", request.BasePrice, request.UnitPrice);
            //    var totalPrice = unitPrice * request.Quantity;
            //    var newOffer = new Offer("", request.BasePrice, request.Quantity, unitPrice, totalPrice, request.UOM);
            //    newOffer.CreatedAT = DateTime.Now;
            //    newOffer.CreatedBy = request.CreatedBy;
            //    newPartList.AddOffer(newOffer);
            //}
            model.CreatedAT = DateTime.Now;
            model.CreatedBy = request.CreatedBy;

            model.AddLoanPartList(newPartList);

            _loanRepository.Update(model);
            var result = await _loanRepository.SaveChangesAsync();
            if (result == 0)
                return new ReturnDto<LoanPartListQueryModel>
                {
                    Data = null,
                    Count = 0,
                    IsSuccess = false,
                    Message = "Someting went wrong when Loan Order Part List created",
                };
            var returnData = new LoanPartListQueryModel
            {
                Id = model.Id,
                PartId = newPartList.PartId,
                Quantity = newPartList.Quantity,
                UOM = newPartList.UOM,
                SerialNo = newPartList.SerialNo,
                RID = newPartList.RID,
                ShipDate = newPartList.ShipDate,
                ShippingReference = newPartList.ShippingReference,
                ReceivedDate = newPartList.ReceivedDate,
                ReceivingReference = newPartList.ReceivingReference,
                ReceivingDefect = newPartList.ReceivingDefect,
                IsDeleted = newPartList.IsDeleted,
                IsInvoiced = newPartList.IsInvoiced,
            };
            return new ReturnDto<LoanPartListQueryModel>
            {
                Data = returnData,
                Count = 1,
                IsSuccess = true,
                Message = "Loan Order Part List created successfully"
            };
        }
    }
    public class AddLoanPartListComman : IRequest<ReturnDto<LoanPartListQueryModel>>
    {
        public Guid LoanId { get; set; }
        public Guid Id { get; set; }
        public Guid PartId { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public List<string>? Description { get; set; }
        public double BasePrice { get; set; }
        public double? UnitPrice { get; set; }
        public string Currency { get; set; }


        [JsonIgnore]
        public string? CreatedBy { get; private set; }
        public void SetCreatedBy(string createdBy) { CreatedBy = createdBy; }
    }
}
