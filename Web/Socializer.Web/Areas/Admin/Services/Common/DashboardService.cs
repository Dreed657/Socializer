namespace Socializer.Web.Areas.Admin.Services.Common
{
    using System.Threading.Tasks;

    using Socializer.Services.Data.Groups;
    using Socializer.Services.Data.Posts;
    using Socializer.Services.Data.Users;
    using Socializer.Web.Areas.Admin.Services.Groups;
    using Socializer.Web.Areas.Admin.Services.Users;
    using Socializer.Web.ViewModels.Dashboard;

    public class DashboardService : IDashboardService
    {
        private readonly IAdminUsersService userAdminService;
        private readonly IPostsService postsService;
        private readonly IAdminGroupsService adminGroupService;

        public DashboardService(IAdminUsersService userAdminService, IPostsService postsService, IAdminGroupsService adminGroupService)
        {
            this.userAdminService = userAdminService;
            this.postsService = postsService;
            this.adminGroupService = adminGroupService;
        }

        public async Task<DbHomeViewModel> GetHomeData()
        {
            var model = new DbHomeViewModel()
            {
                PostsCount = await this.postsService.GetPostsCountAsync(),
                UsersCount = await this.userAdminService.GetUserCountAsync(),
                GroupCount = await this.adminGroupService.GetGroupsCountAsync(),
                GroupRequestsCount = await this.adminGroupService.GetPendingRequestsCountAsync(),
            };

            return model;
        }
    }
}
