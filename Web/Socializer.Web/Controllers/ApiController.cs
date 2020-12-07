namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Socializer.Services.Data.Posts;
    using Socializer.Web.ViewModels.Posts;

    [ApiController]
    public class ApiController : Controller
    {
        private readonly IPostsService postsService;

        public ApiController(IPostsService postsService)
        {
            this.postsService = postsService;
        }

        [HttpGet("api/post/comments")]
        public async Task<IEnumerable<CommentViewModel>> GetAllComments(int postId)
        {
            return await this.postsService.GetAllComments(postId);
        }
    }
}
