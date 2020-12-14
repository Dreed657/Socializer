namespace Socializer.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Web.Areas.Admin.Services;

    public class HomeController : DashboardController
    {
        private readonly IDashboardService dashboardService;

        public HomeController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.dashboardService.GetHomeData();
            return this.View(model);
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}
