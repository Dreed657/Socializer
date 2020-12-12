namespace Socializer.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class GroupMember : BaseModel<int>
    {
        [Required]
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        [Required]
        public string MemberId { get; set; }

        public virtual ApplicationUser Member { get; set; }

        public GroupRole Role { get; set; }
    }
}
