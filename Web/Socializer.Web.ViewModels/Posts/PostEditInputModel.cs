namespace Socializer.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Models.Enums;

    public class PostEditInputModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        public PrivacyStatus Status { get; set; }
    }
}
