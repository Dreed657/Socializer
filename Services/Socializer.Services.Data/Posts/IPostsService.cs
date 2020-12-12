namespace Socializer.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int?> CreateAsync(PostInputModel model, string userId, int groupId = 0);

        Task<bool> DeleteAsync(int id);

        Task<T> GetPostByIdAsync<T>(int postId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> GetPostsCountAsync();

        Task<bool> AddComment(string content, int postId, string userId);

        Task<IEnumerable<CommentViewModel>> GetAllComments(int postId);

        Task LikeAsync(int postId, string userId);

        Task UnLikeAsync(int postId, string userId);

        Task<bool> IsLikedAsync(int postId, string userId);
    }
}
