namespace Socializer.Data.Models
{
    using System.Collections.Generic;

    using Socializer.Data.Common.Models;

    public class ChatGroup : BaseModel<int>
    {
        public ChatGroup()
        {
            this.Members = new HashSet<ApplicationUser>();
            this.ChatMessages = new HashSet<ChatMessage>();
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Members { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
