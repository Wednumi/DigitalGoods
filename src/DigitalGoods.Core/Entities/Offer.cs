﻿namespace DigitalGoods.Core.Entities
{
    public class Offer : BaseEntity
    {
        public string Name { get; private set; } = null!;

        public float? Price { get; private set; }

        public int? Discount { get; private set; }

        public string? Discription { get; private set; }

        public int Amount { get; private set; }

        public bool Active { get; private set; }

        public int? UserId { get; private set; }

        public User User { get; private set; } = null!;

        public int? BankAccountId { get; private set; }

        public BankAccount? BankAccount { get; private set; }

        public int? SourceId { get; private set; }

        public Source? Source { get; private set; }

        public ICollection<Tag>? Tags { get; private set; }

        public ICollection<Media>? Medias { get; private set; }

        public ICollection<ActivationCode> ActivationCodes { get; private set; } = null!;

        public ICollection<Order> Sales { get; private set; } = null!;

        private Offer() 
        { }

        public Offer(string name, float? price, int? discount, string? discription, 
            int amount, User user, BankAccount? bankAccount, 
            Source? source, ICollection<Tag>? tags, ICollection<Media>? medias)
        {
            Active = false;
            Name = name;
            Price = price;
            Discount = discount;
            Discription = discription;
            Amount = amount;
            UserId = user.Id;
            User = user;
            BankAccountId = bankAccount?.Id;
            BankAccount = bankAccount;
            SourceId = source?.Id;
            Source = source;
            Tags = tags;
            Medias = medias;
            ActivationCodes = new List<ActivationCode>();
            Sales = new List<Order>();
        }
    }
}