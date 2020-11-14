using Microsoft.AspNetCore.Authorization;

namespace Socializer.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BaseController : Controller
    {
    }
}
