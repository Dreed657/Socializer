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

        public UserService(Cloudinary cloudinary, IRepository<ApplicationUser> userRepo, IRepository<FriendRequest> friendRequestRepo)
        {
            this.cloudinary = cloudinary;
            this.userRepo = userRepo;
            this.friendRequestRepo = friendRequestRepo;
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

        public async Task<bool> UpdateUser(EditUserProfileInputModel model, string userId)        {            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)            {                return false;            }
            user.FirstName = model.FirstName;            user.LastName = model.LastName;            user.Description = model.Description;            user.Gender = model.Gender;

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
            this.userRepo.Update(user);            await this.userRepo.SaveChangesAsync();            return true;        }

        public async Task<string?> GetIdByUserName(string username)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.UserName == username);

            return user?.Id;
        }
    }
}
