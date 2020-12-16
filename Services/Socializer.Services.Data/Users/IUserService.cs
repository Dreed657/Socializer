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

        Task<string> GetIdByUserName(string username);

        Task<IEnumerable<T>> GetAllUsersAsync<T>();
    }
}
