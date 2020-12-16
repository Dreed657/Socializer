namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Dashboard.Groups;
    using Socializer.Web.ViewModels.Groups;

    public interface IGroupService
    {
        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<bool> UpdateGroup(GroupInputModel model, int groupId, string userId);

        Task<bool> IsMemberInGroup(int groupId, string userId);

        Task<bool> IsMemberAdmin(int groupId, string userId);

        Task<string> AddMemberToGroupAsync(int groupId, string userId);

        Task<bool> CreateGroupRequestAsync(GroupRequestInputModel model, ApplicationUser creator);
    }
}
