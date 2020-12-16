namespace Socializer.Web.Areas.Messenger.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Data.Users;
    using Socializer.Web.Hubs;

    public class MessengerService : IMessengerService
    {
        private readonly IHubContext<MessengerHub> messengerHub;
        private readonly IRepository<ApplicationUser> userRepo;

        public MessengerService(IHubContext<MessengerHub> messengerHub, IRepository<ApplicationUser> userRepo)
        {
            this.messengerHub = messengerHub;
            this.userRepo = userRepo;
        }

        public async Task AddUserToGroup(string groupName, string senderId)
        {
            var sender = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == senderId);

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
