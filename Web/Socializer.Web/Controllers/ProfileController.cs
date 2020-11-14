using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Socializer.Services.Data.Profiles;

namespace Socializer.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IProfilesService profilesService;

        public ProfileController(IProfilesService profilesService)
        {
            this.profilesService = profilesService;
        }

        [HttpGet("/Profile/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var model = await this.profilesService.GetProfileByUsername(username);
            return this.View(model);
        }
    }
}
