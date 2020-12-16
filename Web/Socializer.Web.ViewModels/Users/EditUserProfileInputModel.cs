namespace Socializer.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using Socializer.Web.ViewModels.Common;

    public class EditUserProfileInputModel : EditUserInputModel
    {
        [Required]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Profile Image")]
        [DataType(DataType.Upload)]
        public IFormFile ProfileImage { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.Upload)]
        public IFormFile CoverImage { get; set; }
    }
}
