namespace Socializer.Web.ViewModels.Chat
{
    using System.Collections.Generic;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class ChatGroupViewModel : IMapFrom<ChatGroup>
    {
        public ChatGroupViewModel()
        {
            this.ChatMessages = new HashSet<ChatMessageViewModel>();
            this.Members = new HashSet<ShortUserViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ShortUserViewModel> Members { get; set; }

        public ICollection<ChatMessageViewModel> ChatMessages { get; set; }
    }
}
