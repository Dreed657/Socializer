namespace Socializer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
