using System.Linq;
using Socializer.Web.ViewModels.Images;

namespace Socializer.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Friends;
    using Socializer.Services.Data.Images;
    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    public class ProfileController : BaseController
    {
        private readonly IUserService userService;
        private readonly IFriendService friendService;
        private readonly IImagesService imageService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(UserManager<ApplicationUser> userManager, IUserService userService, IFriendService friendService, IImagesService imageService)
        {
            this.userService = userService;
            this.friendService = friendService;
            this.imageService = imageService;
            this.userManager = userManager;
        }

        [HttpGet("/Profile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var profileModel = await this.userService.GetUserByUsernameAsync<ProfileViewModel>(username);

            if (profileModel == null)
            {
                return this.Redirect("/error");
            }

            var userId = await this.userService.GetIdByUserName(username);

            var friends = await this.friendService.GetAllFriendsByUserIdAsync<ShortUserViewModel>(userId);
            var images = await this.imageService.GetAllImageByUserId<ImageViewModel>(userId);

            var model = new ProfileIndexModel()
            {
                ViewModel = profileModel,
                Friends = friends.ToList(),
                Images = images.ToList(),
            };

            return this.View(model);
        }

        [HttpGet("/AddFriend")]
        public async Task<IActionResult> AddFriend(string userId, string returnUrl)
        {
            var senderId = this.userManager.GetUserId(this.User);
            await this.friendService.AddRequestFriendAsync(senderId, userId);

            return this.Redirect(returnUrl);
        }

        [HttpGet("/Approve")]
        public async Task<IActionResult> ApproveRequest(int requestId, string returnUrl)
        {
            await this.friendService.ApproveRequestFriendAsync(requestId);
            return this.Redirect(returnUrl);
        }

        [HttpGet("/Decline")]
        public async Task<IActionResult> DeclineRequest(int requestId, string returnUrl)
        {
            await this.friendService.DeclineRequestFriendAsync(requestId);
            return this.Redirect(returnUrl);
        }
    }
}
