namespace Socializer.Web.ViewModels.Posts
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Socializer.Data.Models.Enums;

    public class PostInputModel
    {
        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Privacy")]
        public PrivacyStatus Privacy { get; set; }

        public IFormFile Image { get; set; }
    }
}
