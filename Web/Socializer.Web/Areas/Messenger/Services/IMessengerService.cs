namespace Socializer.Web.Areas.Messenger.Services
{
    using System.Threading.Tasks;

    public interface IMessengerService
    {
        Task AddUserToGroup(string groupName, string senderId);

        Task SendMessageToUser(string senderId, string message, string groupName);

        Task ReceiveNewMessage(string senderId, string message, string groupName);
    }
}
