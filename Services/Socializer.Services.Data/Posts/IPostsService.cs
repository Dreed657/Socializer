namespace Socializer.Services.Data.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Posts;

    public interface IPostsService
    {
        Task<int?> CreateAsync(PostsInputModel model, string userId);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task Like(int postId, string userId);

        Task UnLike(int postId, string userId);

        Task<bool> IsLiked(int postId, string userId);
    }
}
