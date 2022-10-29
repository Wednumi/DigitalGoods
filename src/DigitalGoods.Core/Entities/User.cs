namespace DigitalGoods.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = null!;

        public string Login { get; private set; } = null!;

        public string Password { get; private set; } = null!;

        public ICollection<Order> Purchases { get; private set; } = null!;

        public ICollection<Offer> Offers { get; private set; } = null!;

        public ICollection<BankAccount> BankAccounts { get; private set; } = new List<BankAccount>();

        public double MoneyAccount { get; private set; }

        private User()
        { }

        public User(string email, string login, string password)
        {
            Email = email;
            Login = login;
            Password = password;
            Purchases = new List<Order>();
            Offers = new List<Offer>();
        }
    }
}
