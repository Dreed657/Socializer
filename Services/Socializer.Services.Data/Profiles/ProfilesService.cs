using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Socializer.Services.Mapping;

namespace Socializer.Services.Data.Profiles
{
    using System.Threading.Tasks;

    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Web.ViewModels.Users;

    public class ProfilesService : IProfilesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepo;

        public ProfilesService(IDeletableEntityRepository<ApplicationUser> userRepo)
        {
            this.userRepo = userRepo;
        }

        public async Task<ProfileViewModel> GetProfileByUsernameAsync(string username)
        {
            return await this.userRepo
                .All()
                .Where(x => x.UserName == username)
                .To<ProfileViewModel>()
                .FirstOrDefaultAsync();
        }
    }
}
