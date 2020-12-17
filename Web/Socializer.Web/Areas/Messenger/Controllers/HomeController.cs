using System.Threading;
using Microsoft.AspNetCore.Identity;
using Socializer.Data.Models;
using Socializer.Web.Areas.Messenger.Services;

namespace Socializer.Web.Areas.Messenger.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    [Authorize]
    [Area("Messenger")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly IChatService chatService;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IUserService userService,
            IChatService chatService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.chatService = chatService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await this.userService.GetAllUsersAsync<ShortUserViewModel>();
            return this.View(users);
        }

        public async Task<IActionResult> StartChat(string userId)
        {
            var loggedInUserId = this.userManager.GetUserId(this.User);
            var groupId = await this.chatService.IsUsersAlreadyInARoom(userId, loggedInUserId);

            return this.RedirectToAction(nameof(this.Chat), new { gId = groupId });
        }

        public async Task<IActionResult> Chat(int gId)
        {
            var model = await this.chatService.GetChatGroupDetails(gId);
            return this.View(model);
        }
    }
}
