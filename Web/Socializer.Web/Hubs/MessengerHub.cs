namespace Socializer.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Socializer.Web.Areas.Messenger.Services;

    public class MessengerHub : Hub
    {
        private readonly IChatService charService;

        public MessengerHub(IChatService charService)
        {
            this.charService = charService;
        }

        public async Task JoinChannel(string groupName, string senderId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
            var message = await this.charService.AddUserToGroup(senderId, groupName);
            await this.Clients.Group(groupName).SendAsync("MemberJoined", message, groupName);
        }

        public async Task SendMessage(string senderId, string message, string groupName)
        {
            await this.charService.SendMessageToGroup(message, senderId, groupName);
        }
    }
}
