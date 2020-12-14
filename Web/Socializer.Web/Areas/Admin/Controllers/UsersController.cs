namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Profiles;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Dashboard.Users;
    using Socializer.Web.ViewModels.Users;

    public class UsersController : DashboardController
    {
        private readonly IUserService userService;
        private readonly IProfilesService profilesService;

        public UsersController(IUserService userService, IProfilesService profilesService)
        {
            this.userService = userService;
            this.profilesService = profilesService;
        }

        public async Task<IActionResult> Index(string username)
        {
            var view = await this.profilesService.GetProfileByUsernameAsync(username);
            var model = new DbDetailUserComplexModel() { ViewModel = view };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DbDetailUserComplexModel model, string userId, string returnUrl)
        {
            var result = await this.userService.DbEditAsync(model.InputModel, userId);

            if (result)
            {
                return this.Redirect(returnUrl);
            }

            this.ModelState.AddModelError("Error", "Problem happen proceeding your request!");
            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> AllUsers()
        {
            var models = await this.userService.GetAllUsersAsync<ShortUserViewModel>();
            return this.View(models);
        }
    }
}
