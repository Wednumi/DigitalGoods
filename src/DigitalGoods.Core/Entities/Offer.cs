using AutoMapper;

namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; set; } = "NO NAME";

        public float? Price { get; set; }

        public int Discount { get; set; }

        public string? Discription { get; set; }

        public int Amount { get; set; }

        public bool Active { get; set; }

        public string UserId { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public int? SourceId { get; set; }

        public Source? Source { get; set; }

        public int? ReceiveMethodId { get; set; }

        public ReceiveMethod? ReceiveMethod { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public ICollection<Media>? Medias { get; set; }

        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        public ICollection<Order> Sales { get; private set; } = null!;

        public ICollection<OfferChange> OfferChanges { get; private set; } = null!;

        public Offer() 
        { }

        public Offer(User user)
        {
            UserId = user.Id;
            User = user;
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

        public Offer GetCopy()
        {
            var copy = new Offer();
            var mapper = GetMapper();
            return mapper.Map<Offer, Offer>(this, copy);
        }

        public void Map(Offer offer)
        {
            var mapper = GetMapper();
            mapper.Map<Offer, Offer>(offer, this);
        }

        private Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Offer, Offer>());
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
