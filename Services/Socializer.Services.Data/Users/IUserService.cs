namespace Socializer.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Users;
    public interface IUserService
    {
        Task<bool> UpdateUser(EditUserProfileInputModel model, string userId);

        Task<T> GetUserByUsernameAsync<T>(string username);

        Task<T> GetUserByIdAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllUsersAsync<T>();

        Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId);

        Task<bool> AddRequestFriendAsync(string senderId, string receiverId);

        Task<bool> ApproveRequestFriendAsync(int requestId);

        Task<bool> DeclineRequestFriendAsync(int requestId);

        Task<bool> CheckFriendStatus(string senderId, string receiverId);

        bool CheckRequestStatus(string senderId, string receiverId);
    }
}
