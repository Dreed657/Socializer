namespace Socializer.Web.Areas.Admin.Services
{
    using System.Threading.Tasks;

    using Socializer.Services.Data.Groups;
    using Socializer.Services.Data.Posts;
    using Socializer.Services.Data.Users;
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

        public async Task<DbHomeViewModel> GetHomeData()
        {
            var model = new DbHomeViewModel()
            {
                PostsCount = await this.postsService.GetPostsCountAsync(),
                UsersCount = await this.userService.GetUserCountAsync(),
                GroupCount = await this.groupService.GetGroupsCountAsync(),
                GroupRequestsCount = await this.groupService.GetPendingRequestsCountAsync(),
            };

            return model;
        }
    }
}
