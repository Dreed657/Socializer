namespace Socializer.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepo;

        public PostsService(IDeletableEntityRepository<Post> postsRepo)
        {
            this.postsRepo = postsRepo;
        }

        public async Task<int?> CreateAsync(PostsInputModel model, string userId)
        {
            var entity = new Post()
            {
                Content = model.Content,
                CreatorId = userId,
            };

            await this.postsRepo.AddAsync(entity);
            await this.postsRepo.SaveChangesAsync();

            return entity?.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = this.postsRepo.All().FirstOrDefault(x => x.Id == id);

            if (entity is null)
            {
                return false;
            }

            this.postsRepo.Delete(entity);
            await this.postsRepo.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.postsRepo
                .All()
                .Include(x => x.Creator)
                .To<T>()
                .ToListAsync();
        }
    }
}
