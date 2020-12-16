namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Dashboard.Groups;
    using Socializer.Web.ViewModels.Groups;

    public interface IGroupService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<T> GetByIdAsync<T>(int id);

        Task<bool> EditGroup(GroupInputModel model, int groupId, string userId);

        Task<bool> DbUpdateGroup(DbEditGroupModel model, int groupId);

        Task<bool> IsMemberInGroup(int groupId, string userId);

        Task<bool> IsMemberAdmin(int groupId, string userId);

        Task<string> AddMemberToGroupAsync(int groupId, string userId);

        Task<bool> CreateGroupRequestAsync(GroupRequestInputModel model, ApplicationUser creator);

        Task<int> GetGroupsCountAsync();

        Task<int> GetPendingRequestsCountAsync();

        Task<IEnumerable<T>> GetAllRequestsAsync<T>();

        Task<T> GetRequestByIdAsync<T>(int id);

        Task ApproveRequestAsync(int requestId);

        Task DeclineRequestAsync(int requestId);
    }
}
