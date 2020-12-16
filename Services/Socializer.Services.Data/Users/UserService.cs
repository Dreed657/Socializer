namespace Socializer.Services.Data.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Common;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Common;
    using Socializer.Web.ViewModels.Dashboard.Users;
    using Socializer.Web.ViewModels.Users;

    public class UserService : IUserService
    {
        private readonly Cloudinary cloudinary;
        private readonly IRepository<ApplicationUser> userRepo;
        private readonly IRepository<FriendRequest> friendRequestRepo;
        private readonly IRepository<Friend> friendsRepo;

        public UserService(Cloudinary cloudinary, IRepository<ApplicationUser> userRepo, IRepository<FriendRequest> friendRequestRepo, IRepository<Friend> friendsRepo)
        {
            this.cloudinary = cloudinary;
            this.userRepo = userRepo;
            this.friendRequestRepo = friendRequestRepo;
            this.friendsRepo = friendsRepo;
        }

        public async Task<T> GetUserByUsernameAsync<T>(string username)
        {
            return await this.userRepo
                .All()
                .Where(x => x.UserName == username)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetUserByIdAsync<T>(string userId)
        {
            return await this.userRepo.All().Where(x => x.Id == userId).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllUsersAsync<T>()
        {
            return await this.userRepo.All().To<T>().ToListAsync();
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
                .Any(x => (x.ReceiverId == receiverId && x.SenderId == senderId) || ((x.ReceiverId == senderId && x.SenderId == receiverId) && x.IsFriend));
        }

        public bool CheckRequestStatus(string senderId, string receiverId)
        {
            return this.friendRequestRepo.All()
                .Any(x => (x.ReceiverId == receiverId && x.SenderId == senderId) || ((x.ReceiverId == senderId && x.SenderId == receiverId) && x.Status == Status.Pending));
        }

        public async Task<bool> UpdateUser(EditUserProfileInputModel model, string userId)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            if (model.FirstName != user.FirstName)
            {
                user.FirstName = model.FirstName;
            }

            if (model.LastName != user.LastName)
            {
                user.LastName = model.LastName;
            }

            if (model.Description != user.Description)
            {
                user.Description = model.Description;
            }

            if (model.Gender != user.Gender)
            {
                user.Gender = model.Gender;
            }

            if (model.ProfileImage != null)
            {
                var imageName = Guid.NewGuid().ToString();
                var imageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.ProfileImage, imageName);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    user.ProfileImage = new Image()
                    {
                        Url = imageUrl,
                        Name = imageName,
                        Creator = user,
                    };
                }
            }

            if (model.CoverImage != null)
            {
                var imageName = Guid.NewGuid().ToString();
                var imageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.CoverImage, imageName);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    user.CoverImage = new Image()
                    {
                        Url = imageUrl,
                        Name = imageName,
                        Creator = user,
                    };
                }
            }

            this.userRepo.Update(user);
            await this.userRepo.SaveChangesAsync();
            return true;
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
