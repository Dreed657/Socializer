namespace Socializer.Data.Models
{
    using Socializer.Data.Common.Models;

    public class UserChatGroup : BaseModel<int>
    {
        public int ChatGroupId { get; set; }

        public virtual ChatGroup ChatGroup { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
