namespace Socializer.Web.ViewComponents
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    public class FeedViewComponent : ViewComponent
    {
        private readonly IPostsService postService;

        public FeedViewComponent(IPostsService postService)
        {
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var models = await this.postService.GetAllAsync<PostViewModel>();

            return this.View(models.OrderByDescending(x => x.CreatedOn));
        }
    }
}
