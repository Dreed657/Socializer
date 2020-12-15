using Microsoft.AspNetCore.Authorization;

namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postsService;

        public ApiController(UserManager<ApplicationUser> userManager, IPostsService postsService)
        {
            this.userManager = userManager;
            this.postsService = postsService;
        }

        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetAllComments(int postId)
        {
            var data = await this.postsService.GetAllComments(postId);

            if (!data.Any())
            {
                return this.NoContent();
            }

            return this.Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            var result = await this.postsService.AddComment(content, postId, this.userManager.GetUserId(this.User));

            if (!result)
            {
                return this.NoContent();
            }

            return this.Ok(postId);
        }

        public async Task<IActionResult> Like(int postId)
        {
            await this.postsService.LikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Ok();
        }

        public async Task<IActionResult> UnLike(int postId)
        {
            await this.postsService.UnLikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Ok();
        }
    }
}
