namespace Socializer.Web.Areas.Admin.Services.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Dashboard.Groups;

    public interface IAdminGroupsService
    {
        Task<int> GetGroupsCountAsync();

        Task<int> GetPendingRequestsCountAsync();

        Task<T> GetRequestByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllRequestsAsync<T>();

        Task ApproveRequestAsync(int requestId);

        Task DeclineRequestAsync(int requestId);

        Task<bool> UpdateGroup(DbEditGroupModel model, int groupId);
    }
}
