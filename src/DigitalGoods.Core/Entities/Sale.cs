namespace DigitalGoods.Core.Entities
{
    public class Sale : BaseEntity
    {
        public int OfferId { get; private set; }

        public Offer Offer { get; private set; } = null!;

        public int BuyerId { get; private set; }

        public User Buyer { get; private set; } = null!;

        public DateTime Date { get; private set; }

        public ActivationCode ActivationCode { get; private set; } = null!;

        private Sale() 
        { }

        public Sale(Offer offer, User buyer, DateTime date, ActivationCode activationCode)
        {
            OfferId = offer.Id;
            Offer = offer;
            BuyerId = buyer.Id;
            Buyer = buyer;
            Date = date;
            ActivationCode = activationCode;
        }
    }
}
