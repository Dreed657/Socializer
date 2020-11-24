using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Socializer.Services.Data.Profiles;
using Socializer.Services.Data.Users;
using Socializer.Web.ViewModels.Users;

namespace Socializer.Web.Areas.Admin.Controllers
{
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
            var model = await this.profilesService.GetProfileByUsername(username);
            return this.View(model);
        }

        public async Task<IActionResult> AllUsers()
        {
            var models = await this.userService.GetAllUsers<ShortUserViewModel>();
            return this.View(models);
        }
    }
}
