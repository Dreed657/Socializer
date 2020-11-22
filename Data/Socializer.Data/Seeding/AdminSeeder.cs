namespace Socializer.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Socializer.Common;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Data.Users;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var userService = serviceProvider.GetRequiredService<IUserService>();

            if (await userService.GetUserCount() == 0)
            {
                await this.SeedAdminAsync(userManager);
            }
        }

        public async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync("Administrator01");

            if (admin == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Admin",
                    LastName = "01",
                    UserName = "Administrator01",
                    Email = "admin@gmail.com",
                    Gender = Gender.Male,
                    Birthdate = DateTime.Now,
                };

                await userManager.CreateAsync(user, "password");
                await userManager.AddToRolesAsync(user, new string[] { GlobalConstants.AdministratorRoleName, GlobalConstants.VerifiedRoleName });
            }
        }
    }
}
