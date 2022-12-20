using DigitalGoods.Core.Enums;

namespace DigitalGoods.Core.Entities
{
    public class BankAccount : BaseEntity
    {
        public string UserId { get; private set; } = null!;

        public User User { get; private set; } = null!;

        public string Account { get; private set; } = null!;

        public BankAccountType Type { get; private set; }

        private BankAccount()
        { }

        public BankAccount(User user, string account, BankAccountType type)
        {
            User = user;
            Account = account;
            Type = type;
            UserId = user.Id;
        }
    }
}
