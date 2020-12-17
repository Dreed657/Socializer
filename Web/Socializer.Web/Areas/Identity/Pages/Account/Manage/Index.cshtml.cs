namespace Socializer.Web.Areas.Identity.Pages.Account.Manage
{
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUserService userService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public EditUserProfileInputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);

                var sb = new StringBuilder();

                foreach (var item in this.ModelState.Values)
                {
                    sb.AppendLine($"Value: {item.RawValue} State: {item.ValidationState} |");
                }

                this.StatusMessage = sb.ToString().Trim();
                return this.Page();
            }

            if (!await this.userService.UpdateUser(this.Input, user.Id))
            {
                this.StatusMessage = "Unexpected error when trying to set data.";
                return this.RedirectToPage();
            }

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToAction("index", "profile", new { username = user.UserName });
        }

        private Task LoadAsync(ApplicationUser user)
        {
            this.Input = new EditUserProfileInputModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                BirthDate = user.Birthdate,
                Gender = user.Gender,
                Email = user.Email,
            };

            return Task.CompletedTask;
        }
    }
}
