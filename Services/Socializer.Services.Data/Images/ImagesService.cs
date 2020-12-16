namespace Socializer.Services.Data.Images
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> imageRepo;

        public ImagesService(IRepository<Image> imageRepo)
        {
            this.imageRepo = imageRepo;
        }

        public async Task<IEnumerable<T>> GetAllImageByUserId<T>(string userId)
        {
            return await this.imageRepo
                .All()
                .Where(x => x.CreatorId == userId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<T> GetImageByName<T>(string name)
        {
            return await this.imageRepo.All().Where(x => x.Name == name).To<T>().FirstOrDefaultAsync();
        }
    }
}
