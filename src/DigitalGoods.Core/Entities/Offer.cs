namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; set; } = "NO NAME";

        public float? Price { get; set; }

        public int? Discount { get; set; }

        public string? Discription { get; set; }

        public int Amount { get; set; }

        public bool Active { get; set; }

        public string UserId { get; private set; } = null!;

        public int? SourceId { get; set; }

        public Source? Source { get; set; }

        public int? ReceiveMethodId { get; set; }

        public ReceiveMethod? ReceiveMethod { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public ICollection<Media>? Medias { get; set; }

        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        public ICollection<Order> Sales { get; private set; } = null!;

        public ICollection<OfferChange> OfferChanges { get; private set; } = null!;

        private Offer() 
        { }

        public Offer(User user)
        {
            UserId = user.Id;
            Active = false;
            Tags = new List<Tag>();
            Medias = new List<Media>();
            ActivationCodes = new List<ActivationCode>();
            Sales = new List<Order>();
            OfferChanges = new List<OfferChange>();
        }

        public bool IsOwnerValid(User owner)
        {
            return UserId == owner.Id;
        }
    }
}
