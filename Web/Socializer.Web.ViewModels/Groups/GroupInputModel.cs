namespace Socializer.Web.ViewModels.Groups
{
    using System.ComponentModel.DataAnnotations;

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

        public string Visibility { get; set; }
    }
}
