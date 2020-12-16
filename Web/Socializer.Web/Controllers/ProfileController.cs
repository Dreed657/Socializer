namespace Socializer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    public class ProfileController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpGet("/Profile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var model = await this.userService.GetUserByUsernameAsync<ProfileViewModel>(username);

            if (model == null)
            {
                return this.Redirect("/error");
            }

            return this.View(model);
        }

        [HttpGet("Friends")]
        public IActionResult Friends()
        {
            return this.View();
        }

        [HttpGet("/AddFriend")]
        public async Task<IActionResult> AddFriend(string userId, string returnUrl)
        {
            var senderId = this.userManager.GetUserId(this.User);
            await this.userService.AddRequestFriendAsync(senderId, userId);

            return this.Redirect(returnUrl);
        }

        [HttpGet("/Approve")]
        public async Task<IActionResult> ApproveRequest(int requestId, string returnUrl)
        {
            await this.userService.ApproveRequestFriendAsync(requestId);
            return this.Redirect(returnUrl);
        }

        [HttpGet("/Decline")]
        public async Task<IActionResult> DeclineRequest(int requestId, string returnUrl)
        {
            await this.userService.DeclineRequestFriendAsync(requestId);
            return this.Redirect(returnUrl);
        }
    }
}
