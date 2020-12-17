namespace Socializer.Web.ViewModels.Common
{
    using Socializer.Data.Models;
    using Socializer.Services.Mapping;

    public class UserPartialViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
