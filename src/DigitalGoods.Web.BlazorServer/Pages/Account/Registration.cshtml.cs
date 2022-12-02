using DigitalGoods.Core.Entities;
using DigitalGoods.Core.Interfaces;
using DigitalGoods.Web.BlazorServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace DigitalGoods.Web.BlazorServer.Pages.Account
{
    public class RegistrationModel : PageModel
    {
        private readonly IAccountManager accountManager;

        [BindProperty]
        public SignUpCredential SignUpCredential { get; set; } = new SignUpCredential();

        public RegistrationModel(IAccountManager accountManager)
        {
            this.accountManager = accountManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            if (SignUpCredential.IsPasswordConfirmed())
            {
                var user = new User(SignUpCredential.Email, SignUpCredential.UserName);

                var result = await accountManager.RegisterAsync(user, SignUpCredential.Password);
                if (result.Succeeded)
                {
                    return LocalRedirect("/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }                    
                }
            }
            return Page();
        }
    }

    public class SignUpCredential : Credential
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password must be confirmed")]
        [MinLength(8, ErrorMessage = "Minimum password length is 8")]
        [DataType(DataType.Password)]
        [Display(Name = "Password confirmation")]
        public string PasswordConfirmation { get; set; } = String.Empty;

        public bool IsPasswordConfirmed()
        {
            return Password == PasswordConfirmation;
        }
    }
}
