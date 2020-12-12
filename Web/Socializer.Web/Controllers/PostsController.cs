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

        [Route("/{postId}")]
        public async Task<IActionResult> Details(int postId)
        {
            var model = await this.postsService.GetPostByIdAsync<PostViewModel>(postId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostInputModel model, string returnUrl, int groupId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.postsService.CreateAsync(model, user.Id, groupId);

            return this.Redirect(returnUrl);
        }

        [HttpPost]
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
