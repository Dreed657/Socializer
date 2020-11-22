﻿namespace Socializer.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;

    public interface IUserService
    {
        Task<T> GetUserById<T>(string userId);

        Task<IEnumerable<T>> GetAllUsers<T>();

        Task<int> GetUserCount();

        Task<ApplicationUser> GetUserById(string id);

        Task<IEnumerable<T>> GetAllFriendRequestsAsync<T>(string receiverId);

        Task<bool> AddRequestFriendAsync(string senderId, string receiverId);

        Task<bool> ApproveRequestFriendAsync(int requestId);

        Task<bool> DeclineRequestFriendAsync(int requestId);

        bool CheckFriendStatus(string senderId, string receiverId);

        bool CheckRequestStatus(string senderId, string receiverId);
    }
}
