using Microsoft.AspNetCore.Identity;
using Socializer.Data.Models;

namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    [Route("api")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPostsService postsService;

        public ApiController(UserManager<ApplicationUser> userManager, IPostsService postsService)
        {
            this.userManager = userManager;
            this.postsService = postsService;
        }

        [HttpGet("post/comments")]
        public async Task<IEnumerable<CommentViewModel>> GetAllComments(int postId)
        {
            return await this.postsService.GetAllComments(postId);
        }

        [HttpGet("Like")]
        public async Task Like(int postId)
        {
            await this.postsService.LikeAsync(postId, this.userManager.GetUserId(this.User));
        }

        [HttpGet("UnLike")]
        public async Task UnLike(int postId)
        {
            await this.postsService.UnLikeAsync(postId, this.userManager.GetUserId(this.User));
        }
    }
}
