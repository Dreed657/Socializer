namespace Socializer.Web.Areas.Admin.Services.Users
{
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Dashboard.Users;

    public interface IAdminUsersService
    {
        Task<bool> DbUpdateAsync(DbUserInputModel model, string userId);

        Task<int> GetUserCountAsync();
    }
}
