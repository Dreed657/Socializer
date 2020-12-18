namespace Socializer.Web.Areas.Messenger.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.Hubs;
    using Socializer.Web.ViewModels.Chat;

    public class ChatService : IChatService
    {
        private readonly IHubContext<MessengerHub> messengerHub;
        private readonly IRepository<ApplicationUser> userRepo;
        private readonly IRepository<ChatGroup> chatGroupRepo;
        private readonly IRepository<ChatMessage> chatMessageRepo;

        public ChatService(
            IHubContext<MessengerHub> messengerHub,
            IRepository<ApplicationUser> userRepo,
            IRepository<ChatGroup> chatGroupRepo,
            IRepository<ChatMessage> chatMessageRepo)
        {
            this.messengerHub = messengerHub;
            this.userRepo = userRepo;
            this.chatGroupRepo = chatGroupRepo;
            this.chatMessageRepo = chatMessageRepo;
        }

        // TODO: CHECK IF MEMBER IS PART OF THE GROUP OR REDIRECT
        public async Task<string> AddUserToGroup(string userId, string groupName)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);

            return $"{user.FirstName} {user.LastName} has joined {groupName}!";
        }

        public async Task<IEnumerable<T>> GetAllGroupMessages<T>(int groupId)
        {
            var group = await this.chatGroupRepo.All().FirstOrDefaultAsync(x => x.Id == groupId);

            return group.ChatMessages.AsQueryable().To<T>().ToList();
        }

        public async Task<ChatGroupViewModel> GetChatGroupDetails(int groupId)
        {
            return await this.chatGroupRepo.All().To<ChatGroupViewModel>().FirstOrDefaultAsync(x => x.Id == groupId);
        }

        public async Task<int?> IsUsersAlreadyInARoom(string userId, string loggedInUserId)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);
            var loggedInUser = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == loggedInUserId);
            var group = await this.chatGroupRepo.All().FirstOrDefaultAsync(x => x.Members.Contains(user) && x.Members.Contains(loggedInUser));

            if (group != null)
            {
                return group?.Id;
            }
            else
            {
                group = new ChatGroup()
                {
                    Name = Guid.NewGuid().ToString(),
                };
                await this.chatGroupRepo.AddAsync(group);

                group.Members.Add(user);
                group.Members.Add(loggedInUser);
                await this.chatGroupRepo.SaveChangesAsync();

                return group.Id;
            }
        }

        public async Task<bool> SendMessageToGroup(string message, string userId, string groupName)
        {
            var group = await this.chatGroupRepo.All().FirstOrDefaultAsync(x => x.Name == groupName);

            if (group == null)
            {
                return false;
            }

            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            group.ChatMessages.Add(new ChatMessage()
            {
                ChatGroup = group,
                Content = message,
                Sender = user,
            });

            await this.chatGroupRepo.SaveChangesAsync();
            return true;
        }
    }
}
