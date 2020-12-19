namespace Socializer.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Socializer.Common;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;

    public class AdminSeeder : ISeeder
    {
        public static async Task SeedAdminAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            var admin = await userManager.FindByNameAsync("Administrator01");

            var profileImage = await dbContext.Images.FirstOrDefaultAsync(x => x.Name == "Default_Profile");
            var coverImage = await dbContext.Images.FirstOrDefaultAsync(x => x.Name == "Default_Cover");

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
                    ProfileImage = profileImage,
                    CoverImage = coverImage,
                };

                await userManager.CreateAsync(user, "password");
                await userManager.AddToRolesAsync(user, new string[] { GlobalConstants.AdministratorRoleName, GlobalConstants.VerifiedRoleName });
            }
        }

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Users.Any())
            {
                await SeedAdminAsync(dbContext, userManager);
            }
        }
    }
}
