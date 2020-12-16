namespace Socializer.Services.Data.Friends
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFriendService
    {
        Task<IEnumerable<T>> GetAllFriendsByUserIdAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId);

        Task<bool> AddRequestFriendAsync(string senderId, string receiverId);

        Task<bool> ApproveRequestFriendAsync(int requestId);

        Task<bool> DeclineRequestFriendAsync(int requestId);

        Task<bool> CheckFriendStatus(string senderId, string receiverId);

        Task<bool> CheckRequestStatus(string senderId, string receiverId);
    }
}
