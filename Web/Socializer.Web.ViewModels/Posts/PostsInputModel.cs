namespace Socializer.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class PostsInputModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
