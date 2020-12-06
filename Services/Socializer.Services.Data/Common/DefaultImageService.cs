namespace Socializer.Services.Data.Common
{
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;

    public class DefaultImageService : IDefaultImageService
    {
        private readonly IRepository<Image> imageRepo;

        public DefaultImageService(IRepository<Image> imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        public async Task<Image> GetDefaultCoverImage()
        {
            return await this.imageRepo.All().FirstOrDefaultAsync(x => x.Name == "Default_Cover");
        }

        public async Task<Image> GetDefaultProfileImage()
        {
            return await this.imageRepo.All().FirstOrDefaultAsync(x => x.Name == "Default_Profile");
        }
    }
}
