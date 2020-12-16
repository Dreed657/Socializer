namespace Socializer.Web.ViewModels.Users
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class VeryShortUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
