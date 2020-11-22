namespace Socializer.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepo;
        private readonly IRepository<FriendRequest> friendRequestRepo;
        private readonly IRepository<Friend> friendsRepo;

        public UserService(IRepository<ApplicationUser> userRepo, IRepository<FriendRequest> friendRequestRepo, IRepository<Friend> friendsRepo)
        {
            this.userRepo = userRepo;
            this.friendRequestRepo = friendRequestRepo;
            this.friendsRepo = friendsRepo;
        }

        public async Task<T> GetUserById<T>(string userId)
        {
            return await this.userRepo.All().Where(x => x.Id == userId).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllUsers<T>()
        {
            return await this.userRepo.All().To<T>().ToListAsync();
        }

        public Task<int> GetUserCount()
        {
            return this.userRepo.All().CountAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId)
        {
            return await this.friendRequestRepo
                .All()
                .Where(x => x.ReceiverId == receiverId)
                .Where(x => x.Status == Status.Pending)
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

            await this.AddFriend(entity);
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

        public bool CheckFriendStatus(string senderId, string receiverId)
        {
            return this.friendsRepo.All()
                .Any(x => (x.ReceiverId == receiverId && x.SenderId == senderId) || (x.ReceiverId == senderId && x.SenderId == receiverId) && x.IsFriend);
        }

        public bool CheckRequestStatus(string senderId, string receiverId)
        {
            return this.friendRequestRepo.All()
                .Any(x => (x.ReceiverId == receiverId && x.SenderId == senderId) || (x.ReceiverId == senderId && x.SenderId == receiverId) && x.Status == Status.Pending);
        }

        private async Task AddFriend(FriendRequest request)
        {
            var entity = await this.friendsRepo.All()
                .FirstOrDefaultAsync(x => x.ReceiverId == request.ReceiverId && x.SenderId == request.SenderId);

            if (entity == null)
            {
                entity = new Friend()
                {
                    SenderId = request.SenderId,
                    ReceiverId = request.ReceiverId,
                    IsFriend = true,
                };
                await this.friendsRepo.AddAsync(entity);
            }
            else
            {
                entity.IsFriend = false;
            }

            await this.friendsRepo.SaveChangesAsync();
        }
    }
}
