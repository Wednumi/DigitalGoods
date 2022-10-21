namespace DigitalGoods.Core.Entities
{
    public class Media : BaseEntity
    {
        public string Code { get; private set; } = null!;

        public int OfferId { get; private set; }

        public Offer Offer { get; private set; } = null!;

        private Media() 
        { }

        public Media(string code, Offer offer)
        {
            Code = code;
            OfferId = offer.Id;
            Offer = offer;
        }
    }
}
