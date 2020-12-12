namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class GroupCreateRequest : BaseModel<int>
    {
        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public Status Status { get; set; }
    }
}
