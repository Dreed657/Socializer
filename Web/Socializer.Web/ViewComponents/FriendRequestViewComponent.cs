using Socializer.Services.Data.Friends;

namespace Socializer.Web.Infrastructure.ViewComponents
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Socializer.Services.Data.Users;
    using Socializer.Web.ViewModels.Users;

    public class FriendRequestViewComponent : ViewComponent
    {
        private readonly IFriendService friendService;

        public FriendRequestViewComponent(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string receiverId)
        {
            var models = await this.friendService.GetAllFriendRequestsAsync<FriendRequestViewModel>(receiverId);
            return this.View(models);
        }
    }
}
