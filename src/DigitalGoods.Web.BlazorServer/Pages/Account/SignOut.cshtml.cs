using DigitalGoods.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitalGoods.Web.BlazorServer.Pages.Account
{
    public class SignOutModel : PageModel
    {
        private readonly IAccountManager _accountManager;

        public SignOutModel(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var previousPage = Request.Headers["Referer"].ToString();
            await _accountManager.SignOutAsync();
            return Redirect(previousPage);
        }
    }
}
