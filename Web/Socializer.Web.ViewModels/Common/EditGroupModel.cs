namespace Socializer.Web.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class EditGroupModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
