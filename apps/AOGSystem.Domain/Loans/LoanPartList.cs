using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Domain.Loans
{
    public class LoanPartList : BaseEntity
    {
        public Guid PartId { get; private set; }
        public int Quantity { get; private set; }
        public string UOM { get; private set; }
        public string? SerialNo { get; private set; }
        public string? RID { get; private set; }
        public DateTime? ShipDate { get; private set; }
        public string? ShippingReference { get; private set; }
        public DateTime? ReceivedDate { get; private set; }
        public string? ReceivingReference { get; private set; }
        public string? ReceivingDefect { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsInvoiced { get; private set; }


        public void SetPartId(Guid partId) { this.PartId = partId; }
        public void SetQuantity(int quantity) { this.Quantity = quantity; }
        public void SetUOM(string uOM) { this.UOM = uOM; }
        public void SetSerialNo(string? serialNo) { SerialNo = serialNo; }
        public void SetRID(string? rid) { RID = rid; }
        public void SetShipDate(DateTime? shipDate) { ShipDate = shipDate; }
        public void SetShippingReference(string? shippingReference) { ShippingReference = shippingReference; }
        public void SetReceivedDate(DateTime? recievedDate) { ReceivedDate = recievedDate; }
        public void SetReceivingReference(string? recievingReference) { ReceivingReference = recievingReference; }
        public void SetReceivingDefect(string? receivingDefect) { ReceivingDefect = receivingDefect; }
        public void SetIsDeleted(bool isDeleted) { this.IsDeleted = isDeleted; }
        public void SetIsInvoiced(bool isInvoiced) { IsInvoiced = isInvoiced; }

        private readonly List<Offer> offers;
        public IReadOnlyCollection<Offer> Offers => offers;
        protected LoanPartList() : base()
        {
            offers = new List<Offer>();
        }

        public LoanPartList(Guid partId, int quantity, string uOM, string? serialNo, string? rID, DateTime? shipDate, string? shippingReference, DateTime? receivedDate, string? receivingReference, string? receivingDefect, bool isDeleted, bool isInvoiced) : this()
        {
            SetPartId(partId);
            SetQuantity(quantity);
            SetUOM(uOM);
            SetSerialNo(serialNo);
            SetRID(rID);
            SetShipDate(shipDate);
            SetShippingReference(shippingReference);
            SetReceivedDate(receivedDate);
            SetReceivingReference(receivingReference);
            SetReceivingDefect(receivingDefect);
            SetIsDeleted(isDeleted);
            SetIsInvoiced(isInvoiced);
        }

        public LoanPartList(Guid partId, int quantity, string uOM) : this ()
        {
            SetPartId(partId);
            SetQuantity(quantity);
            SetUOM(uOM);
            
        }

        public void AddOffer(Offer offer)
        {
            offers.Add(offer);
        }
        public void AddOffer(string description, int basePrice, int quantity, int unitPrice, int totalPrice, string currency)
        {
            var newItem = new Offer(description, basePrice, quantity, unitPrice, totalPrice, currency);
            AddOffer(newItem);
        }
        public void UpdateOffer(Guid id, string description, int basePrice, int quantity, int unitPrice, int totalPrice, string currency) 
        {
            var exists = offers.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                exists.SetDescription(description);
                exists.SetBasePrice(basePrice); 
                exists.SetQuantity(quantity);
                exists.SetUnitPrice(unitPrice);
                exists.SetTotalPrice(totalPrice);
                exists.SetCurrency(currency);
            }
        }

        public void RemoveOffer(Offer offer) 
        {
            offers.Remove(offer);
        }

        public void RemoveOffer(Guid id) 
        {
            var exists = offers.FirstOrDefault(x => x.Id == id);
            if (exists != null)
            {
                RemoveOffer(exists);
            }
        }
    }
}
