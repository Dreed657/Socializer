<<<<<<< HEAD
﻿namespace Socializer.Web.Areas.Messenger.Services
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
=======
﻿using System;
using Socializer.Services.Data.Users;

namespace Socializer.Web.Areas.Messenger.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
>>>>>>> main
    using Socializer.Web.Hubs;

    public class MessengerService : IMessengerService
    {
        private readonly IHubContext<MessengerHub> messengerHub;
<<<<<<< HEAD
        private readonly IRepository<ApplicationUser> userRepo;

        public MessengerService(IHubContext<MessengerHub> messengerHub, IRepository<ApplicationUser> userRepo)
        {
            this.messengerHub = messengerHub;
            this.userRepo = userRepo;
=======
        private readonly IUserService userService;

        public MessengerService(IHubContext<MessengerHub> messengerHub, IUserService userService)
        {
            this.messengerHub = messengerHub;
            this.userService = userService;
>>>>>>> main
        }

        public async Task AddUserToGroup(string groupName, string senderId)
        {
<<<<<<< HEAD
            var sender = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == senderId);

            await this.messengerHub.Clients.Group(groupName).SendAsync("MemberJoined", senderId, $"New user joined {sender.UserName}", groupName);
=======
            var sender = await this.userService.GetUserByIdAsync(senderId);

            await this.messengerHub.Clients.Group(groupName).SendAsync("ReceiveMessage", senderId, $"New user joined {sender.UserName}", groupName);
>>>>>>> main
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
