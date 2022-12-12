using AutoMapper;
using DigitalGoods.Core.Enums;

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

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public ReceiveMethod? ReceiveMethod { get; set; }

        public ICollection<Tag> Tags { get; set; } = null!;

        public ICollection<Media> Medias { get; private set; } = null!;

        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        public ICollection<Order> Sales { get; private set; } = null!;

        public ICollection<OfferChange> OfferChanges { get; private set; } = null!;

        private Offer() 
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
