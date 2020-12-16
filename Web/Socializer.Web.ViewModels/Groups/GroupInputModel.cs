namespace Socializer.Web.ViewModels.Groups
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using Socializer.Data.Models.Enums;

    public class GroupInputModel
    {
        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Group Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Privacy level")]
        public PrivacyStatus Status { get; set; }

        public IFormFile CoverImage { get; set; }
    }
}
