using Socializer.Services.Data.Groups;
using Socializer.Services.Data.Posts;
using Socializer.Services.Data.Users;

namespace Socializer.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Dashboard;

    public class DashboardService : IDashboardService
    {
        private readonly IUserService userService;
        private readonly IPostsService postsService;
        private readonly IGroupService groupService;

        public DashboardService(IUserService userService, IPostsService postsService, IGroupService groupService)
        {
            this.userService = userService;
            this.postsService = postsService;
            this.groupService = groupService;
        }

        public async Task<DashboardHomeViewModel> GetHomeData()
        {
            var model = new DashboardHomeViewModel()
            {
                PostsCount = await this.postsService.GetPostsCount(),
                UsersCount = await this.userService.GetUserCount(),
                GroupRequestsCount = await this.groupService.GetRequestsCount(),
            };

            return model;
        }
    }
}
