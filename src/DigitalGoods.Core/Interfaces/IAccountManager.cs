using DigitalGoods.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DigitalGoods.Core.Interfaces
{
    public interface IAccountManager
    {
        public Task<IdentityResult> RegisterAsync(User user, string password);

        public Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent = false);

        public Task SignOutAsync();

        public Task<SignInResult> CheckForSignInAsync(User user, string password);

        public Task<User> GetUserByClaimsAsync(ClaimsPrincipal principal);
    }
}
