namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Users;
    using Socializer.Web.Areas.Admin.Services.Users;
    using Socializer.Web.ViewModels.Dashboard.Users;
    using Socializer.Web.ViewModels.Users;

    public class UsersController : DashboardController
    {
        private readonly IUserService userService;
        private readonly IAdminUsersService adminUserService;

        public UsersController(IUserService userService, IAdminUsersService adminUserService)
        {
            this.userService = userService;
            this.adminUserService = adminUserService;
        }

        public async Task<IActionResult> Index(string username)
        {
            var view = await this.userService.GetUserByUsernameAsync<ProfileViewModel>(username);
            var model = new DbDetailUserComplexModel() { ViewModel = view };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DbDetailUserComplexModel model, string userId, string returnUrl)
        {
            var result = await this.adminUserService.DbUpdateAsync(model.InputModel, userId);

            if (result)
            {
                return this.Redirect(returnUrl);
            }

            this.ModelState.AddModelError("Error", "Problem happen proceeding your request!");
            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> AllUsers()
        {
            var models = await this.userService.GetAllUsersAsync<VeryShortUserViewModel>();
            return this.View(models);
        }
    }
}
