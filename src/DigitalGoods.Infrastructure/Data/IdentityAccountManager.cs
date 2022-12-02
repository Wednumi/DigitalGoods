using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DigitalGoods.Infrastructure.Data
{
    public class IdentityAccountManager : IAccountManager
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public IdentityAccountManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> SignInAsync(string userName, string password, bool isPersistent = false)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, false);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> CheckForSignInAsync(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }

        public async Task<User> GetUserByClaimsAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }
    }
}
