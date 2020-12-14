namespace Socializer.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Socializer.Web.Areas.Messenger.Services;

    public class MessengerHub : Hub
    {
        private readonly IMessengerService messengerService;

        public MessengerHub(IMessengerService messengerService)
        {
            this.messengerService = messengerService;
        }

        public async Task JoinChannel(string groupName, string senderId)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, groupName);
            await this.messengerService.AddUserToGroup(groupName, senderId);
        }

        public async Task SendMessage(string senderId, string message, string groupName)
        {
            await this.Clients.All.SendAsync("ReceiveMessage", senderId, message, groupName);
        }

        public async Task ReceiveMessage(string senderId, string message, string groupName)
        {
            await this.Clients.User(senderId).SendAsync("SendMessage", senderId, message, groupName);
        }
    }
}
