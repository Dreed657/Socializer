namespace Socializer.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;

    public interface IUserService
    {
        //Task<ApplicationUser> GetUserById(string id);

        Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId);

        Task<bool> AddRequestFriendAsync(string senderId, string receiverId);

        Task<bool> ApproveRequestFriendAsync(int requestId);

        Task<bool> DeclineRequestFriendAsync(int requestId);

        Task<bool> CheckFriendStatus(string senderId, string receiverId);
    }
}
