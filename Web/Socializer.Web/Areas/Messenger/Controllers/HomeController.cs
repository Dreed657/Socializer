namespace Socializer.Web.Areas.Messenger.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Area("Messenger")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Chat(string groupName)
        {
            return this.View();
        }
    }
}
