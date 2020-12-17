namespace Socializer.Web.ViewModels.Chat
{
    using System;

    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Users;

    public class ChatMessageViewModel : IMapFrom<ChatMessage>
    {
        public string Content { get; set; }

        public ShortUserViewModel Sender { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
