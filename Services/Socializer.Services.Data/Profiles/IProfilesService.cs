using System.Threading.Tasks;
using Socializer.Web.ViewModels.Users;

namespace Socializer.Services.Data.Profiles
{
    public interface IProfilesService
    {
        Task<ProfileViewModel> GetProfileByUsername(string username);
    }
}
