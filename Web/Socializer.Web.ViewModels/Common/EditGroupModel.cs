namespace Socializer.Web.ViewModels.Common
{
    using System.ComponentModel.DataAnnotations;

    public class EditGroupModel
    {
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
