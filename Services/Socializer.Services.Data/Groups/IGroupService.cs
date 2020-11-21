namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Groups;

    public interface IGroupService
    {
        Task<T> GetById<T>(int id);

        Task<IEnumerable<T>> GetAll<T>();

        Task<bool> CreateGroupRequest(GroupRequestInputModel model, ApplicationUser creator);

        Task<int> GetRequestsCount();

        Task<IEnumerable<T>> GetAllRequests<T>();

        Task<T> GetRequestById<T>(int id);

        Task ApproveRequest(int requestId);

        Task DeclineRequest(int requestId);
    }
}
