namespace Socializer.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Common;

    [Area("Admin")]
    [Route("Dashboard/[Controller]/[Action]")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardController : Controller
    {
    }
}
