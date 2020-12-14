namespace Socializer.Services.Data.Profiles
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Socializer.Data.Common.Repositories;
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;
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
