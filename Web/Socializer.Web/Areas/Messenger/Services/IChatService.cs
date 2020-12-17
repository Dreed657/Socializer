namespace Socializer.Web.Areas.Messenger.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Chat;

    public interface IChatService
    {
        Task<ChatGroupViewModel> GetChatGroupDetails(int groupId);

        Task<int?> IsUsersAlreadyInARoom(string userId, string loggedInUserId);

        Task<IEnumerable<T>> GetAllGroupMessages<T>(int groupId);

        Task<string> AddUserToGroup(string userId, string groupName);

        Task<bool> SendMessageToGroup(string message, string userId, string groupName);
    }
}
