namespace Socializer.Web.Areas.Admin.Services.Users
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Common;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Dashboard.Users;

    public class AdminUsersService : IAdminUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<ApplicationUser> userRepo;

        public AdminUsersService(UserManager<ApplicationUser> userManager, IRepository<ApplicationUser> userRepo)
        {
            this.userManager = userManager;
            this.userRepo = userRepo;
        }

        public async Task<bool> DbUpdateAsync(DbUserInputModel model, string userId)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Description = model.Description;
            user.IsDeleted = model.IsDeleted;
            user.Gender = model.Gender;

            if (model.IsVerified && !await this.userManager.IsInRoleAsync(user, GlobalConstants.VerifiedRoleName))
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.VerifiedRoleName);
            }
            else if (!model.IsVerified && await this.userManager.IsInRoleAsync(user, GlobalConstants.VerifiedRoleName))
            {
                await this.userManager.RemoveFromRoleAsync(user, GlobalConstants.VerifiedRoleName);
            }

            if (model.IsDeveloper && !await this.userManager.IsInRoleAsync(user, GlobalConstants.DeveloperRoleName))
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.DeveloperRoleName);
            }
            else if (!model.IsDeveloper && await this.userManager.IsInRoleAsync(user, GlobalConstants.DeveloperRoleName))
            {
                await this.userManager.RemoveFromRoleAsync(user, GlobalConstants.DeveloperRoleName);
            }

            await this.userRepo.SaveChangesAsync();
            return true;
        }

        public Task<int> GetUserCountAsync()
        {
            return this.userRepo.All().CountAsync();
        }
    }
}
