namespace Socializer.Services.Data.Friends
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;

    public class FriendService : IFriendService
    {
        private readonly IRepository<FriendRequest> friendRequestRepo;
        private readonly IRepository<Friend> friendRepo;
        private readonly IRepository<ApplicationUser> userRepo;

        public FriendService(IRepository<FriendRequest> friendRequestRepo, IRepository<Friend> friendRepo, IRepository<ApplicationUser> userRepo)
        {
            this.friendRequestRepo = friendRequestRepo;
            this.friendRepo = friendRepo;
            this.userRepo = userRepo;
        }

        public async Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId)
        {
            return await this.friendRequestRepo
                .All()
                .Where(x => x.ReceiverId == receiverId && x.Status == Status.Pending)
                .To<T>()
                .ToListAsync();
        }

        public async Task<bool> AddRequestFriendAsync(string senderId, string receiverId)
        {
            var request = await this.friendRequestRepo
                .All()
                .FirstOrDefaultAsync(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            if (request == null)
            {
                request = new FriendRequest()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Status = Status.Pending,
                };

                await this.friendRequestRepo.AddAsync(request);
            }
            else
            {
                request.Status = Status.Pending;
                return false;
            }

            await this.friendRequestRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ApproveRequestFriendAsync(int requestId)
        {
            var entity = await this.friendRequestRepo.All().FirstOrDefaultAsync(x => x.Id == requestId);

            if (entity == null)
            {
                return false;
            }

            var friend = new Friend()
            {
                SenderId = entity.SenderId,
                ReceiverId = entity.ReceiverId,
            };

            await this.friendRepo.AddAsync(friend);
            await this.friendRepo.SaveChangesAsync();

            entity.Status = Status.Approved;
            await this.friendRequestRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeclineRequestFriendAsync(int requestId)
        {
            var entity = await this.friendRequestRepo.All().FirstOrDefaultAsync(x => x.Id == requestId);

            if (entity == null)
            {
                return false;
            }

            entity.Status = Status.Decline;
            await this.friendRequestRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckFriendStatus(string senderId, string receiverId)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == receiverId);

            return user.Friends.Any(x => x.SenderId == senderId);
        }

        public async Task<bool> CheckRequestStatus(string senderId, string receiverId)
        {
            return await this.friendRequestRepo
                .All()
                .AnyAsync(x => ((x.ReceiverId == receiverId && x.SenderId == senderId) || (x.ReceiverId == senderId && x.SenderId == receiverId)) && x.Status == Status.Pending);
        }

        public async Task<IEnumerable<T>> GetAllFriendsByUserIdAsync<T>(string userId)
        {
            var temp = await this.userRepo
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

            return temp.Friends.Select(x => x.Sender).AsQueryable().To<T>().ToList();
        }
    }
}
