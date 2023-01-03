using DigitalGoods.Core.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DigitalGoods.Core.Entities
{
    public class User : IdentityUser
    {
        public double MoneyAccount { get; set; }

        [NoMap]
        public float? AverageRating { get; set; }

        public ICollection<Order> Purchases { get; private set; } = new List<Order>();

        public ICollection<Offer> Offers { get; private set; } = new List<Offer>();

        public ICollection<BankAccount> BankAccounts { get; private set; } = new List<BankAccount>();

        private User()
        { }

        public User(string email, string userName)
        {
            Email = email;
            UserName = userName;
        }
    }
}
