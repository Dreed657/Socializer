namespace Socializer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    public class PostsController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postsService;

        public PostsController(UserManager<ApplicationUser> userManager, IPostsService postsService)
        {
            this.userManager = userManager;
            this.postsService = postsService;
        }

        public async Task<IActionResult> Index()
        {
            var models = await this.postsService.GetAllAsync<PostViewModel>();
            return this.View(models.OrderByDescending(x => x.CreatedOn));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostsInputModel model, string returnUrl)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            return this.Redirect(await this.postsService.CreateAsync(model, user.Id) is null ? "Error" : returnUrl);
        }

        public async Task<IActionResult> Delete(int postId, string returnUrl)
        {
            if (!await this.postsService.DeleteAsync(postId))
            {
                return this.Redirect("Error");
            }

            return this.Redirect(returnUrl);
        }
    }
}
