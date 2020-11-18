namespace Socializer.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Common;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
