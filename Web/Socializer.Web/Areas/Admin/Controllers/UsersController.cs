using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Socializer.Services.Data.Users;
using Socializer.Web.ViewModels.Users;

namespace Socializer.Web.Areas.Admin.Controllers
{
    public class UsersController : DashboardController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> AllUsers()
        {
            var models = await this.userService.GetAllUsers<ShortUserViewModel>();
            return this.View(models);
        }
    }
}
