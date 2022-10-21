namespace DigitalGoods.Core.Entities
{
    public class ActivationCode : BaseEntity
    {
        public string Code { get; private set; } = null!;

        public int? OfferId { get; private set; }

        public Offer Offer { get; private set; } = null!;

        public bool Activated { get; private set; }

        private ActivationCode() 
        { }

        public ActivationCode(string code,  Offer offer)
        {
            Code = code;
            OfferId = offer.Id;
            Offer = offer;
            Activated = false;
        }
    }
}
