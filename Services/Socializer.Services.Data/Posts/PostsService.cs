namespace Socializer.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper.Configuration.Conventions;
    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Post> postsRepo;
        private readonly IRepository<PostLike> likeRepo;

        public PostsService(Cloudinary cloudinary, IDeletableEntityRepository<Post> postsRepo, IRepository<PostLike> likesRepo)
        {
            this.cloudinary = cloudinary;
            this.postsRepo = postsRepo;
            this.likeRepo = likesRepo;
        }

        public async Task<int?> CreateAsync(PostsInputModel model, string userId, int groupId = 0)
        {
            var entity = new Post()
            {
                Content = model.Content,
                CreatorId = userId,
            };

            if (groupId != 0)
            {
                entity.GroupId = groupId;
            }

            if (model.Image != null)
            {
                var imageName = Guid.NewGuid().ToString();
                var imageUrl = await ApplicationCloudinary.UploadImage(this.cloudinary, model.Image, imageName);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    entity.Image = new Image()
                    {
                        Url = imageUrl,
                        Name = imageName,
                        CreatorId = userId,
                    };
                }
            }

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

        public async Task<int> GetPostsCountAsync()
        {
            return await this.postsRepo.All().CountAsync();
        }

        public async Task<bool> AddComment(string content, int postId, string userId)
        {
            var post = await this.postsRepo.All().FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            post.Comments.Add(new Comment() { Content = content, Post = post, CreatorId = userId });
            await this.postsRepo.SaveChangesAsync();

            return true;
        }

        public async Task LikeAsync(int postId, string userId)
        {
            var entity = await this.likeRepo
                .All()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            if (entity == null)
            {
                entity = new PostLike()
                {
                    PostId = postId,
                    UserId = userId,
                    IsLiked = true,
                };

                await this.likeRepo.AddAsync(entity);
            }
            else
            {
                entity.IsLiked = true;
            }

            await this.likeRepo.SaveChangesAsync();
        }

        public async Task UnLikeAsync(int postId, string userId)
        {
            var entity = await this.likeRepo
                .All()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            entity.IsLiked = false;

            await this.likeRepo.SaveChangesAsync();
        }

        public async Task<bool> IsLikedAsync(int postId, string userId)
        {
            var entity = await this.likeRepo
                .All()
                .FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

            return entity?.IsLiked ?? false;
        }

        public async Task<T> GetPostByIdAsync<T>(int postId)
        {
            return await this.postsRepo.All().Where(x => x.Id == postId).To<T>().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CommentViewModel>> GetAllComments(int postId)
        {
            var post = await this.postsRepo
                .All()
                .To<PostViewModel>()
                .FirstOrDefaultAsync(x => x.Id == postId);

            return post.Comments;
        }
    }
}
