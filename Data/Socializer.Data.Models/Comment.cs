namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
