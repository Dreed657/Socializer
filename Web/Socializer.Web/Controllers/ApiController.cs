﻿namespace Socializer.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Socializer.Data.Models;
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
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetAllComments(int postId)
        {
            var data = await this.postsService.GetAllComments(postId);

            if (!data.Any())
            {
                return this.BadRequest();
            }

            return this.Ok(data);
        }

        [HttpPost("post/AddComment")]
        public async Task<IActionResult> AddComment(int postId, string content)
        {
            var result = await this.postsService.AddComment(content, postId, this.userManager.GetUserId(this.User));

            if (!result)
            {
                return this.NotFound();
            }

            return this.Ok(postId);
        }

        [HttpGet("post/like")]
        public async Task<IActionResult> Like(int postId)
        {
            await this.postsService.LikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Ok();
        }

        [HttpGet("post/unlike")]
        public async Task<IActionResult> UnLike(int postId)
        {
            await this.postsService.UnLikeAsync(postId, this.userManager.GetUserId(this.User));
            return this.Ok();
        }
    }
}
