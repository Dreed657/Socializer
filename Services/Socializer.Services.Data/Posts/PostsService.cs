namespace Socializer.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    using AutoMapper.Configuration.Conventions;
    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Data.Models.Enums;
    using Socializer.Services.Mapping;
    using Socializer.Web.ViewModels.Posts;

    public class PostsService : IPostsService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Post> postsRepo;
        private readonly IRepository<PostLike> likeRepo;
        private readonly IDeletableEntityRepository<Comment> commentRepo;
        private readonly IRepository<ApplicationUser> userRepo;

        public PostsService(
            Cloudinary cloudinary,
            IDeletableEntityRepository<Post> postsRepo,
            IRepository<PostLike> likesRepo,
            IDeletableEntityRepository<Comment> commentRepo,
            IRepository<ApplicationUser> userRepo)
        {
            this.cloudinary = cloudinary;
            this.postsRepo = postsRepo;
            this.likeRepo = likesRepo;
            this.commentRepo = commentRepo;
            this.userRepo = userRepo;
        }

        public async Task<int?> CreateAsync(PostInputModel model, string userId, int groupId = 0)
        {
            var entity = new Post()
            {
                Content = model.Content,
                CreatorId = userId,
                Privacy = model.Privacy,
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
            return await this.commentRepo.All()
                .Where(x => x.PostId == postId)
                .OrderByDescending(x => x.CreatedOn)
                .To<CommentViewModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<PostViewModel>> GetFeedByUserIdAsync(string userId)
        {
            var user = await this.userRepo.All().FirstOrDefaultAsync(x => x.Id == userId);
            var posts = new List<PostViewModel>();

            posts.AddRange(user.Posts.Where(x => x.Privacy != PrivacyStatus.InProfile).AsQueryable().To<PostViewModel>());

            foreach (var friend in user.Friends.Select(x => x.Sender))
            {
                posts.AddRange(friend.Posts.Where(x => x.Privacy != PrivacyStatus.InProfile && x.Group == null).AsQueryable().To<PostViewModel>());
            }

            foreach (var group in user.Groups.Select(x => x.Group))
            {
                posts.AddRange(group.Posts.Where(x => x.Privacy != PrivacyStatus.InGroup).AsQueryable().To<PostViewModel>());
            }

            return posts.OrderByDescending(x => x.CreatedOn).ToList();
        }

        public async Task<bool> UpdatePost(PostEditInputModel model, int postId)
        {
            var post = await this.postsRepo.All().FirstOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            post.Content = model.Content;

            this.postsRepo.Update(post);
            await this.postsRepo.SaveChangesAsync();

            return true;
        }
    }
}
