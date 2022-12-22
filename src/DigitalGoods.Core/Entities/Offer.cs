using DigitalGoods.Core.Attributes;
using DigitalGoods.Core.Enums;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity, INotifyPropertyChanged
    {
        private const string _defaultName = "NO NAME";

        public event PropertyChangedEventHandler? PropertyChanged;

        [MustNotBe(null, _defaultName)]
        public string Name { get; set; } = _defaultName;

        [MustNotBe(new object?[] { null })]
        public float? Price { get; set; }

        public int Discount { get; set; }

        public string? Discription { get; set; }

        public int Amount { get; set; }

        [NoMap]
        public string UserId { get; private set; } = null!;

        [NoMap]
        public User User { get; private set; } = null!;

        public int? CategoryId { get; private set; }

        public Category? Category { get; private set; }

        [MustNotBe(new object?[] {null})]
        public ReceiveMethod? ReceiveMethod { get; set; }

        public OfferState State { get; private set; }

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
            State = OfferState.NotFilled;
            Tags = new List<Tag>();
            Medias = new List<Media>();
            ActivationCodes = new List<ActivationCode>();
            Sales = new List<Order>();
            OfferChanges = new List<OfferChange>();
        }

        public Offer(User user)
            : this(user.Id)
        {
            User = user;
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

        public Media? GetPreview()
        {
            return Medias.FirstOrDefault(m => m.IsPreview is true);
        }

        public void UpdateState()
        {
            var filled = ConditionChecker.Check(this);
            OfferState newState = filled
                ? OfferState.DeActivated
                : OfferState.NotFilled;
            this.State = this.State == OfferState.Active && filled
                ? OfferState.Active
                : newState;
        }

        public void Activate() => State = OfferState.Active;

        public void Deactivate() => State = OfferState.DeActivated;
    }
}
