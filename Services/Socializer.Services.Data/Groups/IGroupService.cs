namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Groups;

    public interface IGroupService
    {
        Task<T> GetByIdAsync<T>(int id);

        bool IsMemberInGroup(int groupId, string userId);

        Task<bool> AddMemberToGroupAsync(int groupId, string userId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> CreateGroupRequestAsync(GroupRequestInputModel model, ApplicationUser creator);

        Task<int> GetGroupsCountAsync();

        Task<int> GetPendingRequestsCountAsync();

        Task<IEnumerable<T>> GetAllRequestsAsync<T>();

        Task<T> GetRequestByIdAsync<T>(int id);

        Task ApproveRequestAsync(int requestId);

        Task DeclineRequestAsync(int requestId);
    }
}
