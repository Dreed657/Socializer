namespace Socializer.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class PostEditInputModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }
    }
}
