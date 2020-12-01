namespace Socializer.Web.ViewModels.Users
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Socializer.Web.ViewModels.Common;

    public class EditUserProfileInputModel : EditUserInputModel
    {
        [Required]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
