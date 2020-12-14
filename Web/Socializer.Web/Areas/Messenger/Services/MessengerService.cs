namespace Socializer.Web.Areas.Messenger.Services
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Socializer.Services.Data.Users;
    using Socializer.Web.Hubs;

    public class MessengerService : IMessengerService
    {
        private readonly IHubContext<MessengerHub> messengerHub;
        private readonly IUserService userService;

        public MessengerService(IHubContext<MessengerHub> messengerHub, IUserService userService)
        {
            this.messengerHub = messengerHub;
            this.userService = userService;
        }

        public async Task AddUserToGroup(string groupName, string senderId)
        {
            var sender = await this.userService.GetUserByIdAsync(senderId);

            await this.messengerHub.Clients.Group(groupName).SendAsync("ReceiveMessage", senderId, $"New user joined {sender.UserName}", groupName);
        }

        public Task ReceiveNewMessage(string senderId, string message, string groupName)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageToUser(string senderId, string message, string groupName)
        {
            throw new NotImplementedException();
        }
    }
}
