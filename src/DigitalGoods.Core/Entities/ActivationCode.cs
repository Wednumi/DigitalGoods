namespace DigitalGoods.Core.Entities
{
    public class ActivationCode : BaseEntity
    {
        public string Code { get; private set; } = null!;

        public int OfferId { get; private set; }

        public Offer Offer { get; private set; } = null!;

        public int? OrderId { get; private set; }

        public Order? Order { get; set; }

        private ActivationCode() 
        { }

        public ActivationCode(string code, Offer offer)
        {
            Offer = offer;
            OfferId = offer.Id;
            Code = code;
        }
    }
}
