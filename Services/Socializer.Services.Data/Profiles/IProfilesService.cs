namespace Socializer.Services.Data.Profiles
{
    using System.Threading.Tasks;

    using Socializer.Web.ViewModels.Users;

    public interface IProfilesService
    {
        Task<ProfileViewModel> GetProfileByUsernameAsync(string username);
    }
}
