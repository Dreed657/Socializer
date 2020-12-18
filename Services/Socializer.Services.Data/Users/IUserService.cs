namespace Socializer.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Users;
    public interface IUserService
    {
        Task<bool> UpdateUser(EditUserProfileInputModel model, string userId);

        Task<T> GetUserByUsernameAsync<T>(string username);

        Task<T> GetUserByIdAsync<T>(string userId);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        Task<string?> GetIdByUserName(string username);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

        Task<IEnumerable<T>> GetAllUsersAsync<T>();
    }
}
