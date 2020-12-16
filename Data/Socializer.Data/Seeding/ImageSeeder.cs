namespace Socializer.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Models;

    public class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync();

            await this.CreateImage(dbContext, user, "Default_Cover", @"https://via.placeholder.com/728x90.png");
            await this.CreateImage(dbContext, user, "Default_Profile", @"https://image-placeholder.com/images/actual-size/75x75.png");
            await this.CreateImage(dbContext, user, "Default_Group_Cover", @"https://via.placeholder.com/728x90.png");
        }

        public async Task CreateImage(ApplicationDbContext dbContext, ApplicationUser user, string name, string url)
        {
            var image = await dbContext.Images.FirstOrDefaultAsync(x => x.Name == name);

            if (image == null)
            {
                image = new Image()
                {
                    Url = url,
                    Name = name,
                    Creator = user,
                };

                await dbContext.Images.AddAsync(image);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
