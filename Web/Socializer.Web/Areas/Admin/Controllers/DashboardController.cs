using System.Threading.Tasks;
using Socializer.Services.Data.Groups;
using Socializer.Web.Areas.Admin.Services;
using Socializer.Web.ViewModels.Groups.Dashboard;

namespace Socializer.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Common;

    [Area("Admin")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class DashboardController : Controller
    {
        private readonly IGroupService groupService;
        private readonly IDashboardService dashboardService;

        public DashboardController(IGroupService groupService, IDashboardService dashboardService)
        {
            this.groupService = groupService;
            this.dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.dashboardService.GetHomeData();
            return this.View(model);
        }

        public async Task<IActionResult> GroupRequests()
        {
            var models = await this.groupService.GetAllRequests<GroupCreateRequestViewModel>();
            return this.View(models);
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
