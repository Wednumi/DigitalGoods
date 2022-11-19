using DigitalGoods.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace DigitalGoods.Core.Interfaces
{
    public interface IAccountManager
    {
        public Task<IdentityResult> Register(User user, string password);

        public Task<SignInResult> SignIn(string userName, string password, bool isPersistent = false);

        public Task<SignInResult> CheckForSignInAsync(User user, string password);
    }
}
