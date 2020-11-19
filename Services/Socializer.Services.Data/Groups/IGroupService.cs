namespace Socializer.Services.Data.Groups
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Groups;

    public interface IGroupService
    {
        Task<bool> CreateGroupRequest(GroupRequestInputModel model, ApplicationUser creator);

        Task<IEnumerable<T>> GetAllRequests<T>();

        Task<int> GetRequestsCount();

        Task<IEnumerable<T>> GetAll<T>();
    }
}
