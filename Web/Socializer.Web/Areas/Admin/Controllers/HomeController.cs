namespace Socializer.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/admin/[Controller]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.Ok("Admin home controller");
        }
    }
}
