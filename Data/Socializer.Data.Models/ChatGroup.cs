namespace Socializer.Data.Models
{
    using System.Collections.Generic;

    using Socializer.Data.Common.Models;

    public class ChatGroup : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<UserChatGroup> Members { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
