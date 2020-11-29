namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;
    using Socializer.Data.Models.Enums;

    public class GroupMember : BaseModel<int>
    {
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public string MemberId { get; set; }

        public virtual ApplicationUser Member { get; set; }

        public GroupRole Role { get; set; }
    }
}
