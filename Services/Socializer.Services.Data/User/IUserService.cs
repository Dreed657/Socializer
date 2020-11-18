namespace Socializer.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;

    public interface IUserService
    {
        Task<ApplicationUser> GetUserById(string id);

        Task<int> GetUserCount();

        Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId);

        Task<bool> AddRequestFriendAsync(string senderId, string receiverId);

        Task<bool> ApproveRequestFriendAsync(int requestId);

        Task<bool> DeclineRequestFriendAsync(int requestId);

        bool CheckFriendStatus(string senderId, string receiverId);

        bool CheckRequestStatus(string senderId, string receiverId);
    }
}
