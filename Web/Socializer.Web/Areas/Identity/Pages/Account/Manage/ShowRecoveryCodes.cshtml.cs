namespace Socializer.Web.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "<Pending>")]
    public class ShowRecoveryCodesModel : PageModel
    {
        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            if (this.RecoveryCodes == null || this.RecoveryCodes.Length == 0)
            {
                return this.RedirectToPage("./TwoFactorAuthentication");
            }

            return this.Page();
        }
    }
}
