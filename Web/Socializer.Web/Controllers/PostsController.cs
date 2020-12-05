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

        [Route("/{postId}")]
        public async Task<IActionResult> Details(int postId)
        {
            var model = await this.postsService.GetPostByIdAsync<PostViewModel>(postId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostsInputModel model, string returnUrl, int groupId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.postsService.CreateAsync(model, user.Id, groupId);

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> AddComment(int postId, string content)
        {
            var result = await this.postsService.AddComment(content, postId, this.userManager.GetUserId(this.User));

            if (!result)
            {
                return this.BadRequest();
            }

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(int postId, string returnUrl)
        {
            if (!await this.postsService.DeleteAsync(postId))
            {
                return this.Redirect("Error");
            }

            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> Like(int postId, string returnUrl)
        {
            await this.postsService.LikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Redirect(returnUrl);
        }

        public async Task<IActionResult> UnLike(int postId, string returnUrl)
        {
            await this.postsService.UnLikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Redirect(returnUrl);
        }
    }
}
