namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        [Required]
        public string Url { get; set; }

        public string Name { get; set; }

        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }
    }
}
