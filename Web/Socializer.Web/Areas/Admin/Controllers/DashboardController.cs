namespace Socializer.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/admin/[Controller]")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return this.Ok("This is the dashboard");
        }
    }
}
