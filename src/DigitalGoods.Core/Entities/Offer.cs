namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public float? Price { get; private set; }

        public int? Discount { get; private set; }

        public string? Discription { get; private set; }

        public int Amount { get; private set; }

        public bool Active { get; private set; }

        public string? UserId { get; private set; }

        public User User { get; private set; } = null!;

        public int? SourceId { get; private set; }

        public Source? Source { get; private set; }

        public int? ReceiveMethodId { get; private set; }

        public ReceiveMethod? ReceiveMethod { get; private set; }

        public ICollection<Tag>? Tags { get; private set; }

        public ICollection<Media>? Medias { get; private set; }

        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        public ICollection<Order> Sales { get; private set; } = null!;

        public ICollection<OfferChange> OfferChanges { get; private set; } = null!;

        private Offer() 
        { }

        public Offer(string name, float? price, int? discount, string? discription, 
            int amount, User user, Source? source, ICollection<Tag>? tags, 
            ICollection<Media>? medias, ReceiveMethod? receiveMethod)
        {
            Active = false;
            Name = name;
            Price = price;
            Discount = discount;
            Discription = discription;
            Amount = amount;
            UserId = user.Id;
            User = user;
            SourceId = source?.Id;
            Source = source;
            Tags = tags;
            Medias = medias;
            ActivationCodes = new List<ActivationCode>();
            Sales = new List<Order>();
            OfferChanges = new List<OfferChange>();
            ReceiveMethod = receiveMethod;
            ReceiveMethodId = receiveMethod?.Id;
        }
    }
}
