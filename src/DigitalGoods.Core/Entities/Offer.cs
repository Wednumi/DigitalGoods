using DigitalGoods.Core.Attributes;
using DigitalGoods.Core.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; } = "NO NAME";

        public float? Price { get; set; }

        public int Discount { get; set; }

        public string? Discription { get; set; }

        public int Amount { get; set; }

        public bool Active { get; set; }

        [NoMap]
        public string UserId { get; private set; } = null!;

        [NoMap]
        public User User { get; private set; } = null!;

        public int? CategoryId { get; private set; }

        public Category? Category { get; private set; }

        public ReceiveMethod? ReceiveMethod { get; set; }

        public ICollection<Tag> Tags { get; private set; } = null!;

        [NoMap]
        public ICollection<Media> Medias { get; private set; } = null!;

        [NoMap]
        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        [NoMap]
        public ICollection<Order> Sales { get; private set; } = null!;

        [NoMap]
        public ICollection<OfferChange> OfferChanges { get; private set; } = null!;

        private Offer()
        { }

        public Offer(string userId)
        {
            UserId = userId;
            Active = false;
            Tags = new List<Tag>();
            Medias = new List<Media>();
            ActivationCodes = new List<ActivationCode>();
            Sales = new List<Order>();
            OfferChanges = new List<OfferChange>();
        }

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

        public void SetCategory(Category? category)
        {
            CategoryId = category?.Id;
            Category = category;
            NotifyPropertyChanged(nameof(Category));
        }
 
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
