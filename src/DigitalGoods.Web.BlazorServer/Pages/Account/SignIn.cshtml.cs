using DigitalGoods.Core.Interfaces;
using DigitalGoods.Web.BlazorServer.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountManager _accountManager;

        [BindProperty]
        public LoginCredential LoginCredential { get; set; } = new LoginCredential();

        public LoginModel(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _accountManager.SignInAsync(this.LoginCredential.UserName,
                this.LoginCredential.Password,
                this.LoginCredential.RememberMe);

            if (result.Succeeded)
            {
                return LocalRedirect("/");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out.");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login.");
                }
                return Page();
            }
        }
    }

    public class LoginCredential : Credential
    {
        [Display(Name = "Remeber Me")]
        public bool RememberMe { get; set; } = false;
    }
}
