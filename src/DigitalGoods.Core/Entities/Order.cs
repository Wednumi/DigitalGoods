namespace DigitalGoods.Core.Entities
{
    public class Order : BaseEntity
    {
        public int OfferId { get; private set; }

        public Offer? Offer { get; private set; } = null!;

        public string? BuyerId { get; private set; }

        public User Buyer { get; private set; } = null!;

        public DateTime Date { get; private set; }

        public int? ActivationCodeId { get; private set; }

        public ActivationCode? ActivationCode { get; private set; }

        public bool ReceiveConfirmed { get; private set; }

        public int Rate { get; set; }

        public ICollection<Comment> Comments { get; private set; } = null!;

        private Order() 
        { }

        public Order(Offer offer, User buyer, DateTime date, ActivationCode activationCode, int rate = 0)
        {
            OfferId = offer.Id;
            Offer = offer;
            BuyerId = buyer.Id;
            Buyer = buyer;
            Date = date;
            ActivationCode = activationCode;
            ReceiveConfirmed = false;
            Rate = rate;
            Comments = new List<Comment>();
        }
    }
}
