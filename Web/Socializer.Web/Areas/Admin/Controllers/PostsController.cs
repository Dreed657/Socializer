namespace Socializer.Web.Areas.Admin.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    public class PostsController : DashboardController
    {
        private readonly IPostsService postsService;

        public PostsController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        public async Task<IActionResult> AllPosts()
        {
            var models = await this.postsService.GetAllAsync<PostViewModel>();
            return this.View(models);
        }
    }
}
