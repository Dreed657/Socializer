namespace Socializer.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    public class FriendRequestViewComponent : ViewComponent
    {
        private readonly IUserService userService;

        public FriendRequestViewComponent(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string receiverId)
        {
            var models = await this.userService.GetAllFriendRequestsAsync<FriendRequestViewModel>(receiverId);
            return this.View(models);
        }
    }
}
