using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Socializer.Data.Models;

namespace Socializer.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Common;

    public class HomeController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postsService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IPostsService postsService)
        {
            this.userManager = userManager;
            this.postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.userManager.GetUserId(this.User);
            var models = await this.postsService.GetFeedByUserIdAsync(userId);

            return this.View(models);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet("Friends")]
        public IActionResult Friends()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
