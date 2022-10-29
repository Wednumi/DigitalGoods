namespace DigitalGoods.Core.Entities
{
    public class OfferChange : BaseEntity
    {
        public int OfferId { get; private set; }

        public Offer Offer { get; private set; } = null!;

        public string PropertyName { get; set; } = null!;

        public string OldValue { get; set; } = null!;

        public string NewValue { get; set; } = null!;

        private OfferChange() { }

        public OfferChange(Offer offer, string propertyName, string oldValue, string newValue)
        {
            Offer = offer;
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
