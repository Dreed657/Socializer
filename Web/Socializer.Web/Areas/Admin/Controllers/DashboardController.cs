namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Common;
    using Socializer.Web.Areas.Admin.Services;

    [Area("Admin")]
    [Route("Dashboard/[Controller]/[Action]")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardController : Controller
    {
    }
}
