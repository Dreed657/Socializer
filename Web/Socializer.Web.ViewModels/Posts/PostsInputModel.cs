using System.ComponentModel.DataAnnotations;

namespace Socializer.Web.ViewModels.Posts
{
    public class PostsInputModel
    {
        [Required]
        public string Content { get; set; }
    }
}
