namespace DigitalGoods.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = null!;

        public string Login { get; private set; } = null!;

        public string Password { get; private set; } = null!;

        public ICollection<Sale> Purchases { get; private set; } = new List<Sale>();

        public ICollection<Offer> Offers { get; private set; } = new List<Offer>();

        public ICollection<BankAccount> BankAccounts { get; private set; } = new List<BankAccount>();

        private User()
        { }

        public User(string email, string login, string password)
        {
            Email = email;
            Login = login;
            Password = password;
        }
    }
}
